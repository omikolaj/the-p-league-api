using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ThePLeagueAPI.Auth.Errors;
using ThePLeagueAPI.Filters;
using ThePLeagueDomain;
using ThePLeagueDomain.Models.Gallery;
using ThePLeagueDomain.ViewModels.Gallery;
using ThePLeagueDomain.ViewModels;

namespace ThePLeagueAPI.Controllers
{
  [Route("api/[controller]")]
  [Produces("application/json")]
  [ServiceFilter(typeof(ValidateModelStateAttribute))]
  public class GalleryController : ThePLeagueBaseController
  {
    #region Private Fields
    private const string LeagueImage = "LeagueImage";
    private readonly IThePLeagueSupervisor _supervisor;
    private readonly CloudinaryService _cloudinary;

    #endregion

    #region Constructor
    public GalleryController(IThePLeagueSupervisor supervisor, CloudinaryService cloudinaryService)
    {
      this._supervisor = supervisor;
      this._cloudinary = cloudinaryService;
    }

    #endregion

    [HttpGet]
    public async Task<List<LeagueImageViewModel>> Get(CancellationToken ct = default(CancellationToken))
    {
      List<LeagueImageViewModel> leaguePictures = await this._supervisor.GetAllLeagueImagesAsync(ct);

      List<LeagueImageViewModel> orderedLeagueImagesList = leaguePictures.OrderBy(o => o.OrderId).ToList();

      return orderedLeagueImagesList;
    }

    [Authorize]
    [HttpPost("order")]
    public async Task<ActionResult<LeagueImageViewModel>> SaveOrder([FromBody] IEnumerable<LeagueImageViewModel> leagueImages, CancellationToken ct = default(CancellationToken))
    {
      leagueImages = await this._supervisor.UpdateLeagueImagesOrderAsync(leagueImages.ToList(), ct);

      if (leagueImages == null)
      {
        return BadRequest(Errors.AddErrorToModelState(ErrorCodes.LeagueImageNotFound, ErrorDescriptions.LeagueImageSaveOrderFailure, ModelState));
      }

      return new OkObjectResult(leagueImages);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<LeagueImageViewModel>> Create(CancellationToken ct = default(CancellationToken))
    {
      IList<LeagueImageViewModel> uploadedLeagueImages = null;
      List<IFormFile> leagueImages = new List<IFormFile>();
      foreach (IFormFile imageFile in Request.Form.Files)
      {
        leagueImages.Add(imageFile);
      }
      if (leagueImages.Count() > 0)
      {
        uploadedLeagueImages = await this._cloudinary.UploadNewImages<LeagueImageViewModel>(leagueImages, LeagueImage);

        if (uploadedLeagueImages.Any(lI => lI == null))
        {
          return BadRequest(Errors.AddErrorToModelState(ErrorCodes.CloudinaryUpload, ErrorDescriptions.CloudinaryImageUploadFailure, ModelState));
        }

        uploadedLeagueImages = await this._supervisor.AddLeagueImagesAsync(uploadedLeagueImages, ct);
      }

      return new OkObjectResult(uploadedLeagueImages);
    }

    // DELETE api/values/5
    [Authorize]
    [HttpDelete]
    public async Task<ActionResult<bool>> Delete([FromBody] long[] ids)
    {
      for (int i = 0; i < ids.Length; i++)
      {
        LeagueImageViewModel leagueImageToDelete = await this._supervisor.GetLeagueImageByIdAsync(ids[i]);

        if (leagueImageToDelete == null)
        {
          return BadRequest(Errors.AddErrorToModelState(ErrorCodes.LeagueImageNotFound, ErrorDescriptions.LeagueImageDeleteFailure, ModelState));
        }

        DelResResult deletedFromCloudinary = await this._cloudinary.DeleteResource(leagueImageToDelete.CloudinaryPublicId);
        if (deletedFromCloudinary.StatusCode != HttpStatusCode.OK)
        {
          return BadRequest(Errors.AddErrorToModelState(ErrorCodes.CloudinaryDelete, ErrorDescriptions.CloudinaryImageDeleteFailure, ModelState));
        }

        if (!await _supervisor.DeleteLeagueImageAsync(leagueImageToDelete.Id))
        {
          return BadRequest(Errors.AddErrorToModelState(ErrorCodes.LeagueImageDelete, ErrorDescriptions.LeagueImageDeleteFailure, ModelState));
        }
      }

      return new OkObjectResult(true);
    }
  }
}
