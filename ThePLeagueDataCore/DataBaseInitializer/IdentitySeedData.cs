using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ThePLeagueDomain.Models;

namespace ThePLeagueDataCore.DataBaseInitializer
{
    public class IdentitySeedData
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

        public async static Task Populate(IServiceProvider serviceProvider)
        {
            UserManager<ApplicationUser> userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            IConfiguration configuration = serviceProvider.GetService<IConfiguration>();

            await Seed(userManager, roleManager, configuration);
        }

        public async static Task Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
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
                UserName = "npostoloski",
                Email = "npostoloski@icloud.com"
            };

            ApplicationUser sysAdmin = new ApplicationUser
            {
                UserName = "omikolaj",
                Email = "omikolaj1@gmail.com"
            };

            PasswordHasher<ApplicationUser> password = new PasswordHasher<ApplicationUser>();

            if (!userManager.Users.Any(u => u.UserName == admin.UserName))
            {
                string hashed = password.HashPassword(admin, configuration["ThePLeagueAdminInitPassword"]);
                admin.PasswordHash = hashed;

                await userManager.CreateAsync(admin);
                await userManager.AddToRoleAsync(admin, AdminRole);
            }

            if (!userManager.Users.Any(u => u.UserName == sysAdmin.UserName))
            {
                string hashed = password.HashPassword(sysAdmin, configuration["ThePLeagueAdminInitPassword"]);
                sysAdmin.PasswordHash = hashed;

                await userManager.CreateAsync(sysAdmin);
                await userManager.AddToRoleAsync(sysAdmin, AdminRole);
            }

        }
    }

    
}
