using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ThePLeagueAPI.Auth;
using ThePLeagueAPI.Helpers;
using ThePLeagueDataCore;
using ThePLeagueDomain.Models;

namespace ThePLeagueAPI.Configurations
{
  public static class ConfigureIdentity
  {
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
    {
      services.Configure<DataProtectionTokenProviderOptions>(options =>
      {
        options.TokenLifespan = TimeSpan.FromDays(7);
      });

      services.AddIdentity<ApplicationUser, IdentityRole>(options =>
      {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
      })
      .AddTokenProvider(TokenOptionsStrings.RefreshTokenProvider, typeof(DataProtectorTokenProvider<ApplicationUser>))
      .AddRoles<IdentityRole>()
      .AddUserManager<UserManager<ApplicationUser>>()
      .AddRoleManager<RoleManager<IdentityRole>>()
      .AddEntityFrameworkStores<ThePLeagueContext>(); // Tell identity which EF DbContext to use;

      //Configure Claims Identity
      services.AddTransient<GetIdentity>();

      return services;
    }
  }
}