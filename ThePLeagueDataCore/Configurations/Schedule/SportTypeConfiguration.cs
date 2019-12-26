using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDataCore.Configurations.Schedule
{
    public class SportTypeConfiguration
    {
        #region Constructor

        public SportTypeConfiguration(EntityTypeBuilder<SportType> model)
        {
            List<SportType> sports = new List<SportType>();

            string[] sportNames = new string[] { "Basketball", "Volleyball", "Soccer"};

            for (int i = 0; i < sportNames.Length; i++)
            {
                var sportType = new SportType
                {
                    Id = (i + 1).ToString(),
                    Name = sportNames[i]
                };                
                sports.Add(sportType);
            }

            model.HasMany(sport => sport.Leagues)
                .WithOne(league => league.SportType)
                .HasForeignKey(league => league.SportTypeID);

            model.HasData(sports);
        }

        #endregion
    }
}
