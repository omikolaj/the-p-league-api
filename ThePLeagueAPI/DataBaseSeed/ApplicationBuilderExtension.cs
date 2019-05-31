using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ThePLeagueDataCore.DataBaseInitializer;
using ThePLeagueDomain.Models;

namespace ThePLeagueAPI.Extensions
{
  public static class ApplicationBuilderExtensions
  {
    public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
    {
      IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
      try
      {
        UserManager<ApplicationUser> userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
        RoleManager<IdentityRole> roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
        DataBaseInitializer.SeedUsers(userManager, roleManager);
      }
      catch (Exception ex)
      {
        ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
      }
      return app;
    }

  }
}
