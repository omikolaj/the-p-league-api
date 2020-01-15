using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDataCore.DataBaseInitializer
{
    public class SeedData
    {
        #region Fields and Properties

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
        public static void Populate(IServiceProvider serviceProvider)
        {
            ThePLeagueContext dbContext = serviceProvider.GetService<ThePLeagueContext>();

            SeedTeams(dbContext);
        }

        private static void SeedTeams(ThePLeagueContext dbContext)
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
                            if(j % 2 == 0)
                            {
                                var newTeam = new HomeTeam
                                {
                                    Name = TeamNamesA.ElementAt(j),
                                    LeagueID = (i + 1).ToString()
                                };
                                teams.Add(newTeam);
                            }
                            else
                            {
                                var newTeam = new AwayTeam
                                {
                                    Name = TeamNamesA.ElementAt(j),
                                    LeagueID = (i + 1).ToString()
                                };
                                teams.Add(newTeam);
                            }
                        }
                        break;
                    // leagues 2 and 5
                    case 1:
                    case 4:
                        Console.WriteLine("Inside case statement 1, 4");
                        for (int j = 0; j < TeamNamesB.Count; j++)
                        {
                            if (j % 2 == 0)
                            {
                                var newTeam = new HomeTeam
                                {
                                    Name = TeamNamesA.ElementAt(j),
                                    LeagueID = (i + 1).ToString()
                                };
                                teams.Add(newTeam);
                            }
                            else
                            {
                                var newTeam = new AwayTeam
                                {
                                    Name = TeamNamesA.ElementAt(j),
                                    LeagueID = (i + 1).ToString()
                                };
                                teams.Add(newTeam);
                            }
                        }
                        break;
                    // leagues 3 and 6
                    case 2:
                    case 5:
                        for (int j = 0; j < TeamNamesC.Count; j++)
                        {
                            if (j % 2 == 0)
                            {
                                var newTeam = new HomeTeam
                                {
                                    Name = TeamNamesA.ElementAt(j),
                                    LeagueID = (i + 1).ToString()
                                };
                                teams.Add(newTeam);
                            }
                            else
                            {
                                var newTeam = new AwayTeam
                                {
                                    Name = TeamNamesA.ElementAt(j),
                                    LeagueID = (i + 1).ToString()
                                };
                                teams.Add(newTeam);
                            }
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
                    dbContext.Teams.Add(newTeam);
                    dbContext.SaveChanges();
                }
            }
        }

        #endregion
    }
}
