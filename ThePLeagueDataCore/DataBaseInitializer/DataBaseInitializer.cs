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

    private static readonly string AdminRole = "admin";
    private static readonly string SuperUserRole = "superuser";
    private static readonly string UserRole = "user";
    private static readonly string Permission = "permission";
    private static readonly string ModifyPermission = "modify";
    private static readonly string RetrievePermission = "retrieve";
    private static readonly string ViewPermission = "view";

    #endregion

    #region Methods
    public static async void SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      // ThePLeagueRole[] roles = new ThePLeagueRole [] { ThePLeagueRole.User, ThePLeagueRole.Admin };
      string[] roles = new string[] { AdminRole, SuperUserRole, UserRole };

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

          if (role == AdminRole)
          {
            await roleManager.AddClaimAsync(newRole, new Claim(Permission, ModifyPermission));
          }
          else if (role == SuperUserRole)
          {
            await roleManager.AddClaimAsync(newRole, new Claim(Permission, RetrievePermission));
          }
          else
          {
            await roleManager.AddClaimAsync(newRole, new Claim(Permission, ViewPermission));
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
        await userManager.AddToRoleAsync(admin, AdminRole);
        await userManager.AddToRoleAsync(admin, SuperUserRole);
      }

      if (!userManager.Users.Any(u => u.UserName == user.UserName))
      {
        string hashed = password.HashPassword(user, "password");
        user.PasswordHash = hashed;

        await userManager.CreateAsync(user);
        await userManager.AddToRoleAsync(user, UserRole);
      }

    }

    #endregion
  }
}