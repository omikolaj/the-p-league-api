using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.EmailService;
using ThePLeagueAPI.Auth.Jwt;
using ThePLeagueDataCore.Repositories;
using ThePLeagueDataCore.Repositories.Gallery;
using ThePLeagueDataCore.Repositories.Schedule;
using ThePLeagueDataCore.Repositories.TeamSignUp;
using ThePLeagueDomain;
using ThePLeagueDomain.Repositories;
using ThePLeagueDomain.Repositories.Gallery;
using ThePLeagueDomain.Repositories.Merchandise;
using ThePLeagueDomain.Repositories.Schedule;
using ThePLeagueDomain.Repositories.TeamSignUp;
using ThePLeagueDomain.Supervisor;

namespace ThePLeagueAPI.Configurations
{
  public static class ServicesConfiguration
  {
    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
            //Register repository interfaces here      
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>()
                    .AddScoped<IGearItemRepository, GearItemRepository>()
                    .AddScoped<IGearImageRepository, GearImageRepository>()
                    .AddScoped<IGearSizeRepository, GearSizeRepository>()
                    .AddScoped<ILeagueImageRepository, LeagueImageRepository>()
                    .AddScoped<ITeamSignUpRepository, TeamSignUpRepository>()
                    .AddScoped<IPreOrderRepository, PreOrderRepository>()
                    .AddScoped<ILeagueRepository, LeagueRepository>()
                    .AddScoped<ISessionScheduleRepository, SessionScheduleRepository>()
                    .AddScoped<ISportTypeRepository, SportTypeRepository>()
                    .AddScoped<ITeamRepository, TeamRepository>();

      return services;
    }

    public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
    {
      services.AddScoped<IThePLeagueSupervisor, ThePLeagueSupervisor>();
      return services;
    }

    public static IServiceCollection ConfigureEmailSetUp(this IServiceCollection services)
    {
      services.AddScoped<ISendEmailService, SendEmailService>();
      return services;
    }

    public static IServiceCollection AddJsonOptions(this IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            return services;
        }

    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
      IConfigurationSection jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

      services.AddCors();
            
      services.AddCors(options =>
      {
        options.AddPolicy("AllowAll",
          new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()            
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyHeader()
            .Build()
          );

          options.AddPolicy("AllowAllWithClientOrigin",
          new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
            .WithOrigins(configuration["ClientOrigin"])
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyHeader()
            .Build()
          ); ;
      });

      return services;
    }

  }
}