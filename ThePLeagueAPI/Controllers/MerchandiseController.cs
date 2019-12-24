using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ThePLeagueAPI.Auth.Errors;
using ThePLeagueAPI.Filters;
using ThePLeagueDomain;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels.Merchandise;
using CloudinaryDotNet.Actions;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Services.EmailService;
using Microsoft.AspNetCore.Cors;

namespace ThePLeagueAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ServiceFilter(typeof(ValidateModelStateAttribute))]
    public class MerchandiseController : ThePLeagueBaseController
    {
        #region Private Fields
        private readonly IThePLeagueSupervisor _supervisor;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CloudinaryService _cloudinary;
        private readonly ISendEmailService _emailService;

        #endregion

        #region Constructor
        public MerchandiseController(IThePLeagueSupervisor supervisor, UserManager<ApplicationUser> userManager, CloudinaryService cloudinaryService, ISendEmailService emailService)
        {
            this._supervisor = supervisor;
            // this._logger = logger;      
            this._userManager = userManager;
            this._cloudinary = cloudinaryService;
            this._emailService = emailService;
        }

        #endregion

        #region Controllers

        [HttpGet]
        public async Task<List<GearItemViewModel>> Get()
        {
            List<GearItemViewModel> gearItems = await this._supervisor.GetAllGearItemsAsync();

            return gearItems;
        }

        [HttpPost("{id}/pre-order")]
        public async Task<ActionResult<PreOrderViewModel>> PreOrder(PreOrderViewModel preOrderForm, CancellationToken ct = default(CancellationToken))
        {
            GearItemViewModel gearItem = await this._supervisor.GetGearItemByIdAsync(preOrderForm.GearItemId);
            if (gearItem == null)
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.GearItemNotFound, ErrorDescriptions.GearItemFindFailure, ModelState));
            }

            preOrderForm = await this._supervisor.AddPreOrderAsync(preOrderForm, ct);

            //Send an email out to let him know new pre-order has come in
            if (!this._emailService.SendEmail(preOrderForm, gearItem))
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.SendEmail, ErrorDescriptions.SendEmailFailure, ModelState));
            }

            return preOrderForm;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<GearItemViewModel>> Create([FromForm] List<IFormFile> gearImages)
        {
            // Retrieve gearItem object from the request form
            GearItemViewModel gearItem = JsonConvert.DeserializeObject<GearItemViewModel>(Request.Form["gearItem"]);

            //Check if we're uploading images
            if (gearImages.Count() > 0)
            {
                // IList of IFormFile for incoming images to be uploaded
                IList<GearImageViewModel> gearItemImages = await this._cloudinary.UploadNewImages<GearImageViewModel>(gearImages);

                if (gearItemImages.Any(gI => gI == null))
                {
                    return BadRequest(Errors.AddErrorToModelState(ErrorCodes.CloudinaryUpload, ErrorDescriptions.CloudinaryImageUploadFailure, ModelState));
                }

                gearItem.Images = gearItemImages;
            }
            // If we are no uploading images use default image
            else
            {
                gearItem.Images = new GearImageViewModel[]
                {
          new GearImageViewModel
          {
            Url = GearImageViewModel.DefaultGearItemImageUrl,
            Small = GearImageViewModel.DefaultGearItemImageUrl,
            Medium = GearImageViewModel.DefaultGearItemImageUrl,
            Big = GearImageViewModel.DefaultGearItemImageUrl,
            Name = "DefaultImageName"
          }
                };
            }

            // Persist the gearItem along with its successfully uploaded cloudinary images to the database      
            gearItem = await this._supervisor.AddGearItemAsync(gearItem);

            return new JsonResult(gearItem);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            //Check if gear item exists
            GearItemViewModel gearItemToDelete = await this._supervisor.GetGearItemByIdAsync(id);
            if (gearItemToDelete == null)
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.GearItemNotFound, ErrorDescriptions.GearItemDeleteFailure, ModelState));
            }

            if (gearItemToDelete.Images.Count() > 0)
            {
                DelResResult deletedFromCloudinary = await this._cloudinary.DeleteResources(gearItemToDelete);
                if (deletedFromCloudinary.StatusCode != HttpStatusCode.OK)
                {
                    return BadRequest(Errors.AddErrorToModelState(ErrorCodes.CloudinaryDelete, ErrorDescriptions.CloudinaryImageDeleteFailure, ModelState));
                }
            }

            if (!await _supervisor.DeleteGearItemAsync(id))
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.GearItemDelete, ErrorDescriptions.GearItemDeleteFailure, ModelState));
            }

            return new OkObjectResult(true);

        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<GearItemViewModel>> Update([FromForm] List<IFormFile> gearImages)
        {
            // Retrieve gearItem object from the request form
            GearItemViewModel gearItem = JsonConvert.DeserializeObject<GearItemViewModel>(Request.Form["gearItem"]);

            // Check if we are uploading any new images
            if (gearImages.Count() > 0)
            {
                IList<GearImageViewModel> gearItemImages = await this._cloudinary.UploadNewImages<GearImageViewModel>(gearImages);

                if (gearItemImages.Any(gI => gI == null))
                {
                    return BadRequest(Errors.AddErrorToModelState(ErrorCodes.CloudinaryUpload, ErrorDescriptions.CloudinaryImageUploadFailure, ModelState));
                }

                gearItem.NewImages = gearItemImages;
            }

            if (!await this._supervisor.UpdateGearItemAsync(gearItem))
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.GearItemUpdate, ErrorDescriptions.GearItemUpdateFailure, ModelState));
            }

            gearItem = await this._supervisor.GetGearItemByIdAsync(gearItem.Id);

            return new OkObjectResult(gearItem);
        }

        #endregion

    }

}