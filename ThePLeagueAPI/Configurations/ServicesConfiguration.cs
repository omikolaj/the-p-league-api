using Microsoft.Extensions.DependencyInjection;
using ThePLeagueDataCore.Repositories;
using ThePLeagueDomain;
using ThePLeagueDomain.Repositories;
using ThePLeagueDomain.Repositories.Merchandise;
using ThePLeagueDomain.Supervisor;

namespace ThePLeagueAPI.Configurations
{
  public static class ServicesConfiguration
  {
    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
      //Register repository interfaces here
      //AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
      services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>()
              .AddScoped<IGearItemRepository, GearItemRepository>()
              .AddScoped<IGearImageRepository, GearImageRepository>()
              .AddScoped<IGearSizeRepository, GearSizeRepository>()
              .AddScoped<ILeagueImageRepository, LeagueImageRepository>();

      return services;
    }

    public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
    {
      services.AddScoped<IThePLeagueSupervisor, ThePLeagueSupervisor>();
      return services;
    }

    public static IServiceCollection AddMiddleware(this IServiceCollection services)
    {
      services.AddMvc().AddJsonOptions(options =>
      {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
      });

      return services;
    }

    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy("AllowAll",
          new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .Build()
        );
      });

      return services;
    }

  }
}