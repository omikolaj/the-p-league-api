using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using ThePLeagueAPI.Auth.Jwt;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels;
using ThePLeagueDomain.ViewModels.Merchandise;

public class CloudinaryService
{
  private readonly Cloudinary _cloudinary;
  public CloudinaryService(IConfiguration configuration)
  {
    string apiKey = configuration[nameof(VaultKeys.CloudinaryApiKey)];
    string apiSecret = configuration[nameof(VaultKeys.CloudinaryApiSecret)];
    string cloudName = configuration[nameof(VaultKeys.CloudinaryCloudName)];

    Account myAccount = new Account
    {
      ApiKey = apiKey,
      ApiSecret = apiSecret,
      Cloud = cloudName
    };

    this._cloudinary = new Cloudinary(myAccount);
  }

  public async Task<ImageUploadResult> UploadImage(IFormFile file)
  {
    if (file != null)
    {
      var uploadParams = new ImageUploadParams
      {
        File = new FileDescription(file.FileName, file.OpenReadStream()),
        // Transformation = new Transformation().Width(200).Height(200).Crop("thumb").Gravity("face")
      };

      var uploadResult = await _cloudinary.UploadAsync(uploadParams);
      return uploadResult;
    }
    return null;
  }
  public async Task<DelResResult> DeleteResource(string publicId)
  {
    var delParams = new DelResParams()
    {
      PublicIds = new List<string>()
      {
        publicId
      },
      Invalidate = true
    };
    return await _cloudinary.DeleteResourcesAsync(delParams);

  }

  public async Task<DelResResult> DeleteResources(GearItemViewModel gearItem)
  {
    DelResResult result = null;
    foreach (GearImageViewModel gearImage in gearItem.Images)
    {
      result = await this.DeleteResource(gearImage.CloudinaryPublicId);
      if (result.StatusCode != HttpStatusCode.OK)
      {
        return result;
      }
    }
    return result;
  }

  public async Task<T> UploadNewImage<T>(IFormFile file, string gearImageName)
  where T : ImageBaseViewModel, new()
  {
    // Try to upload to Cloudinary
    ImageUploadResult result = await this.UploadImage(file);

    T newImage = (T)Activator.CreateInstance(typeof(T));

    // If successfull create GearImageViewModel
    if (result.StatusCode == HttpStatusCode.OK)
    {
      newImage.CloudinaryPublicId = result.PublicId;
      newImage.Url = result.SecureUri.AbsoluteUri;
      newImage.Small = result.SecureUri.AbsoluteUri;
      newImage.Medium = result.SecureUri.AbsoluteUri;
      newImage.Big = result.SecureUri.AbsoluteUri;
      newImage.Width = result.Width;
      newImage.Height = result.Height;
      newImage.ResourceType = result.ResourceType;
      newImage.Format = result.Format;
      newImage.Name = gearImageName;
    }

    return newImage;
  }

  public async Task<IList<T>> UploadNewImages<T>(IEnumerable<IFormFile> newImages) where T : ImageBaseViewModel, new()
  {
    IList<T> newGearItemImages = new List<T>();
    for (int i = 0; i < newImages.Count(); i++)
    {
      T newGearImage = await this.UploadNewImage<T>(newImages.ElementAt(i), newImages.ElementAt(i).FileName);
      newGearItemImages.Add(newGearImage);
    }

    return newGearItemImages;
  }

}
