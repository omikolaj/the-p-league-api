using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ThePLeagueDataCore;
using ThePLeagueDomain.Models;

namespace ThePLeagueAPI.Configurations
{
  public static class ConfigureIdentity
  {
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
    {
      services.AddIdentity<ApplicationUser, IdentityRole>(options =>
      {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
      })
      .AddRoles<IdentityRole>()
      .AddUserManager<UserManager<ApplicationUser>>()
      .AddRoleManager<RoleManager<IdentityRole>>()
      .AddEntityFrameworkStores<ThePLeagueContext>(); // Tell identity which EF DbContext to use;

      return services;
    }
  }
}