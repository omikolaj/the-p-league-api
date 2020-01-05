using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDataCore.Configurations.Schedule
{
    public class MatchResultsConfigration
    {
        #region Constructor

        public MatchResultsConfigration(EntityTypeBuilder<MatchResult> model)
        {
            model.HasKey(m => m.MatchResultId);        

        }

        #endregion
    }
}
