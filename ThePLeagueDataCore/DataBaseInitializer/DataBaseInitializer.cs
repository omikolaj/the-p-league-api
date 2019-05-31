using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ThePLeagueDomain.Models;

namespace ThePLeagueDataCore.DataBaseInitializer
{
  public enum ThePLeagueRole
  {
    None = 0,
    Admin = 10,
    User = 1
  }
  public class DataBaseInitializer
  {
    #region Fields and Properties

    private static readonly string Admin = "Admin";
    private static readonly string User = "User";
    private static readonly string Permission = "Permission";
    private static readonly string Modify = "Modify";
    private static readonly string View = "View";

    #endregion

    #region Methods
    public static async void SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      // ThePLeagueRole[] roles = new ThePLeagueRole [] { ThePLeagueRole.User, ThePLeagueRole.Admin };
      string[] roles = new string[] { Admin, User };

      foreach (string role in roles)
      {
        if (!roleManager.Roles.Any(r => r.Name == role))
        {
          IdentityRole newRole = new IdentityRole
          {
            Name = role,
            NormalizedName = role.ToUpper()
          };
          await roleManager.CreateAsync(newRole);

          if (role == Admin)
          {
            await roleManager.AddClaimAsync(newRole, new Claim(Permission, Modify));
          }
          else
          {
            await roleManager.AddClaimAsync(newRole, new Claim(Permission, View));
          }
        }
      }

      ApplicationUser admin = new ApplicationUser
      {
        UserName = "oski",
        Email = "abc@xyz.com"
      };

      ApplicationUser user = new ApplicationUser
      {
        UserName = "kaja",
        Email = "def@ghi.com"
      };

      PasswordHasher<ApplicationUser> password = new PasswordHasher<ApplicationUser>();

      if (!userManager.Users.Any(u => u.UserName == admin.UserName))
      {
        string hashed = password.HashPassword(admin, "password");
        admin.PasswordHash = hashed;

        await userManager.CreateAsync(admin);
        await userManager.AddToRoleAsync(admin, Admin);
      }

      if (!userManager.Users.Any(u => u.UserName == user.UserName))
      {
        string hashed = password.HashPassword(user, "password");
        user.PasswordHash = hashed;

        await userManager.CreateAsync(user);
        await userManager.AddToRoleAsync(user, User);
      }

    }

    #endregion
  }
}