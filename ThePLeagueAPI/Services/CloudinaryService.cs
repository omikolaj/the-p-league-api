using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels;
using ThePLeagueDomain.ViewModels.Merchandise;

public class CloudinaryService
{
  private readonly Cloudinary _cloudinary;
  public CloudinaryService(IConfiguration configuration)
  {
    string apiKey = configuration["Cloudinary:APIKEY"];
    string apiSecret = configuration["Cloudinary:APISECRET"];
    string cloudName = configuration["Cloudinary:CloudName"];

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
    // Generate random number from 0-100 for image name
    Random random = new Random();
    int id = random.Next(0, 100);

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
      newImage.Name = $"{gearImageName}_{id}";
    }

    return newImage;
  }

  public async Task<IList<T>> UploadNewImages<T>(IEnumerable<IFormFile> newImages, string name) where T : ImageBaseViewModel, new()
  {
    IList<T> newGearItemImages = new List<T>();
    for (int i = 0; i < newImages.Count(); i++)
    {
      T newGearImage = await this.UploadNewImage<T>(newImages.ElementAt(i), name);
      newGearItemImages.Add(newGearImage);
    }

    return newGearItemImages;
  }

}
