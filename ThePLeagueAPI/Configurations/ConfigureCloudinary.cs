using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ThePLeagueAPI.Configurations
{
  public static class ConfigureCloudinary
  {
    public static IServiceCollection ConfigureCloudinaryService(this IServiceCollection services, IConfiguration configuration)
    {
      // string apiKey = configuration["Cloudinary:APIKEY"];
      // string apiSecret = configuration["Cloudinary:APISECRET"];
      // string cloudName = configuration["Cloudinary:CloudName"];

      // services.Configure<Account>(options =>
      // {
      //   options.ApiKey = apiKey;
      //   options.ApiSecret = apiSecret;
      //   options.Cloud = cloudName;
      // });

      // services.AddTransient<Account>();

      services.AddTransient<CloudinaryService>();

      return services;
    }
  }
}