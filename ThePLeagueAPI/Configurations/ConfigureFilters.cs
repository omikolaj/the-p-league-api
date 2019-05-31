using Microsoft.Extensions.DependencyInjection;
using ThePLeagueAPI.Filters;

namespace ThePLeagueAPI.Configurations
{
  public static class ConfigureFilters
  {
    public static IServiceCollection ConfigureControllersFilters(this IServiceCollection services)
    {
      services.AddScoped<ClaimsIdentityFilter>()
              .AddScoped<ValidateModelStateAttribute>();

      return services;
    }
  }
}