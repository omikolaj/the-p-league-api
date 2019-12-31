using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Schedule;

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
        private static List<string> TeamNamesA
        {
            get
            {
                List<string> teams = new List<string>
                {
                    "Manchester United FC",
                    "Arsenal F.C",
                    "Chelsea F.C",
                    "Manchester City F.C",
                    "Liverpool F.C",
                    "Tottenham Hotspur F.C",
                    "Leicester City F.C",
                    "RTS"
                };

                return teams;
            }
        }

        private static List<string> TeamNamesB
        {
            get
            {
                List<string> teams = new List<string>
                {
                    "FC Barcelona",
                    "Real Madrid C.F",
                    "Atletico Madrid",
                    "Real Betis",
                    "CD Leganes",
                    "Real Sociedad",
                    "Valencia CF",
                    "LKS"
                };

                return teams;
            }
        }

        private static List<string> TeamNamesC
        {
            get
            {
                List<string> teams = new List<string>
                {
                    "Korona Kielce",
                    "Cracovia",
                    "Jagiellonia Białystok",
                    "ŁKS Łódź",
                    "Wisła Płock",
                    "Legia Warszawa",
                    "Lechia Gdańsk",
                    "Lech Poznań"
                };

                return teams;
            }
        }

        #endregion

        #region Methods
        public async static void SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ThePLeagueContext dbContext)
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

        // TODO this should be called from ApplicationBuilderExtensions.cs class. Currently it is not working 
        // https://stackoverflow.com/questions/59539480/unable-to-seed-data-in-asp-net-core-in-a-static-method-due-to-exception-a-secon
            SeedTeams(dbContext);

        }

        public static async void SeedTeams(ThePLeagueContext dbContext)
        {
            List<Team> teams = new List<Team>();

            // 7 because we have 7 leagues
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"Inside the for loop, i is: {i}");
                switch (i)
                {
                    // leagues 1, 4 and 7
                    case 0:
                    case 3:
                    case 6:
                        Console.WriteLine("Inside case statement 0, 3, 6");
                        for (int j = 0; j < TeamNamesA.Count; j++)
                        {
                            var newTeam = new Team
                            {
                                Name = TeamNamesA.ElementAt(j),
                                LeagueID = (i + 1).ToString()
                            };
                            teams.Add(newTeam);                            
                        }
                        break;
                    // leagues 2 and 5
                    case 1:
                    case 4:
                        Console.WriteLine("Inside case statement 1, 4");
                        for (int j = 0; j < TeamNamesB.Count; j++)
                        {
                            var newTeam = new Team
                            {
                                Name = TeamNamesB.ElementAt(j),
                                LeagueID = (i + 1).ToString()
                            };
                            teams.Add(newTeam);                            
                        }
                        break;
                    // leagues 3 and 6
                    case 2:
                    case 5:
                        for (int j = 0; j < TeamNamesC.Count; j++)
                        {
                            var newTeam = new Team
                            {
                                Name = TeamNamesC.ElementAt(j),
                                LeagueID = (i + 1).ToString()
                            };
                            teams.Add(newTeam);
                        }
                        break;
                    default:
                        break;
                }
            }

            if (dbContext.Teams.Count() < teams.Count)
            {
                foreach (Team newTeam in teams)
                {                    
                    await dbContext.Teams.AddAsync(newTeam);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        #endregion
    }
}