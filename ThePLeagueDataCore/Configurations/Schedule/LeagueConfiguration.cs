using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDataCore.Configurations.Schedule
{
    public class LeagueConfiguration
    {
        #region Enum
        enum LeagueNames
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }

        #endregion
        #region Constructor

        public LeagueConfiguration(EntityTypeBuilder<League> model)
        {
            string[] leagueNames = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            List<League> leagues = new List<League>();

            int i = 0;
            foreach (LeagueNames leagueName in Enum.GetValues(typeof(LeagueNames)))
            {
                leagues.Add(new League
                {
                    Id = (i + 1).ToString(),
                    Name = leagueName.ToString(),
                    SportTypeID = AssignLeagueID(leagueName),
                    Type = AssignLeagueType(leagueName),
                    Selected = false
                });
                i++;
            }

            model.HasOne(league => league.SportType)
                .WithMany(sportType => sportType.Leagues)
                .HasForeignKey(league => league.SportTypeID);
            
            model.HasMany(league => league.Teams)
                .WithOne(team => team.League)
                .HasForeignKey(team => team.LeagueID);

            model.HasData(leagues);
        }

        #endregion

        #region Private Methods

        private string AssignLeagueType(LeagueNames leagueName)
        {
            switch (leagueName)
            {
                case LeagueNames.Monday:                    
                case LeagueNames.Tuesday:
                case LeagueNames.Wednesday:
                    return "Basketball";
                case LeagueNames.Thursday:
                case LeagueNames.Friday:
                case LeagueNames.Saturday:
                    return "Volleyball";
                case LeagueNames.Sunday:
                    return "Soccer";
                default:
                    return "";
            }
        }

        private string AssignLeagueID(LeagueNames leagueName)
        {
            switch (leagueName)
            {
                case LeagueNames.Monday:
                case LeagueNames.Tuesday:
                case LeagueNames.Wednesday:
                    return "1";
                case LeagueNames.Thursday:
                case LeagueNames.Friday:
                case LeagueNames.Saturday:
                    return "2";
                case LeagueNames.Sunday:
                    return "3";
                default:
                    return "";
            }
        }

        #endregion
    }
}
