using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDataCore.Configurations.Schedule
{
    public class TeamSessionConfiguration
    {
        #region Constructor

        public TeamSessionConfiguration(EntityTypeBuilder<TeamSession> model)
        {
            // configures the many-to-many relationship

            model.HasKey(ts => new { ts.TeamId, ts.LeagueSessionScheduleId });            
            model.HasOne(ts => ts.Team)
                .WithMany(t => t.TeamsSessions)
                .HasForeignKey(ts => ts.TeamId);
            model.HasOne(ts => ts.LeagueSessionSchedule)
                .WithMany(s => s.TeamsSessions)
                .HasForeignKey(ts => ts.LeagueSessionScheduleId);
        }

        #endregion
    }
}
