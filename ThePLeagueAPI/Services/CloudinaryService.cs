using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

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
}