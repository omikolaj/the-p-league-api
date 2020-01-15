using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDataCore.Configurations.Schedule
{
    public class MatchesConfiguration
    {
        #region Constructor

        public MatchesConfiguration(EntityTypeBuilder<Match> model)
        {
            model.HasOne(match => match.AwayTeam);                

            model.HasOne(match => match.HomeTeam);

            model.HasOne(match => match.League);

            model.HasOne(match => match.MatchResult)
                .WithOne(result => result.Match)
                .HasForeignKey<MatchResult>(result => result.MatchId);

        }

        #endregion
      
    }
}
