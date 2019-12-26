using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDataCore.Configurations.Schedule
{
    public class TeamConfiguration
    {        
        #region Constructor

        public TeamConfiguration(EntityTypeBuilder<Team> model)
        {
            // The data for this model will be generated inside ThePLeagueDataCore.DataBaseInitializer.DatabaseBaseInitializer.cs class
            // When generating data for models in here, you have to provide it with an ID, and it became mildly problematic to consistently get
            // a unique ID for all the teams. In ThePLeagueDataCore.DataBaseInitializer.DatabaseBaseInitializer.cs we can use dbContext to generate
            // unique ids for us for each team.

            model.HasOne(team => team.League)
                .WithMany(league => league.Teams)
                .HasForeignKey(team => team.LeagueID);
        }

        #endregion
       
    }
}
