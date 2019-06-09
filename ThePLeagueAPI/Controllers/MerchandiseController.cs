using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ThePLeagueAPI.Auth;
using ThePLeagueAPI.Auth.Errors;
using ThePLeagueAPI.Auth.Jwt;
using ThePLeagueAPI.Auth.Jwt.JwtFactory;
using ThePLeagueAPI.Filters;
using ThePLeagueAPI.Utilities;
using ThePLeagueDomain;
using ThePLeagueDomain.Converters;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels;
using ThePLeagueDomain.ViewModels.Merchandise;
using Microsoft.AspNetCore.JsonPatch;
using CloudinaryDotNet.Actions;
using System.Net;
using CloudinaryDotNet;

namespace ThePLeagueAPI.Controllers
{
  [Route("api/[controller]")]
  [Produces("application/json")]
  [ApiController]
  // [ServiceFilter(typeof(ValidateModelStateAttribute))]
  public class MerchandiseController : ThePLeagueBaseController
  {
    #region Private Fields
    private readonly IThePLeagueSupervisor _supervisor;
    private readonly ILogger _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly CloudinaryService _cloudinary;

    #endregion

    #region Constructor
    public MerchandiseController(IThePLeagueSupervisor supervisor, UserManager<ApplicationUser> userManager, CloudinaryService cloudinaryService)
    {
      this._supervisor = supervisor;
      // this._logger = logger;      
      this._userManager = userManager;
      this._cloudinary = cloudinaryService;
    }

    #endregion

    #region Controllers

    [HttpGet]
    public async Task<List<GearItemViewModel>> Get()
    {
      List<GearItemViewModel> gearItems = await this._supervisor.GetAllGearItemsAsync();

      return gearItems;
    }

    [HttpPost]
    public async Task<ActionResult<GearItemViewModel>> Create([FromForm] List<IFormFile> gearImages)
    {
      GearItemViewModel gearItemViewModel = null;
      gearItemViewModel = JsonConvert.DeserializeObject<GearItemViewModel>(Request.Form["gearItem"]);

      // Check if we're uploading images
      if (gearImages.Count > 0)
      {
        List<GearImageViewModel> uploadedImages = new List<GearImageViewModel>();
        for (int i = 0; i < gearImages.Count; i++)
        {
          IFormFile gearImage = gearImages[i];
          ImageUploadResult result = await this._cloudinary.UploadImage(gearImage);
          if (result.StatusCode == HttpStatusCode.OK)
          {
            GearImageViewModel newGearImage = new GearImageViewModel()
            {
              CloudinaryId = result.PublicId,
              Url = result.SecureUri.AbsoluteUri,
              Small = result.SecureUri.AbsoluteUri,
              Medium = result.SecureUri.AbsoluteUri,
              Big = result.SecureUri.AbsoluteUri
            };
            uploadedImages.Add(newGearImage);
          }
        }
        gearItemViewModel.Images = gearItemViewModel.Images.Concat(uploadedImages);
      }
      // If we are no uploading images use default image
      else
      {
        gearItemViewModel.Images = new GearImageViewModel[]
        {
          new GearImageViewModel
          {
            Url = GearImageViewModel.DefaultGearItemImage,
            Small = GearImageViewModel.DefaultGearItemImage,
            Medium = GearImageViewModel.DefaultGearItemImage,
            Big = GearImageViewModel.DefaultGearItemImage
          }
        };
      }

      GearItemViewModel gearItemViewModelAdded = await this._supervisor.AddGearItemAsync(gearItemViewModel);

      return new JsonResult(gearItemViewModelAdded);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      return Ok();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update([FromForm] List<IFormFile> gearImages)
    {
      try
      {
        GearItemViewModel gearItemViewModel = JsonConvert.DeserializeObject<GearItemViewModel>(Request.Form["gearItem"]);


      }
      catch (Exception ex)
      {

      }




      return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Replace([FromBody] GearItemViewModel gearItemViewModel)
    {
      var a = gearItemViewModel;
      return Ok();
    }

    #endregion

  }

}