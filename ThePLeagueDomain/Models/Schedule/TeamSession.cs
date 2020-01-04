using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    /// <summary>
    /// Represents a model for the join table between LeagueSessionSchedule model and Team model.
    /// There are many teams and many sessions. Each session can have different number of teams
    /// Each team will part of zero or one Active sessions at a time, but will be part of many
    /// Inactive sessions over time.
    /// </summary>
    [Table("TeamsSessions")]
    public class TeamSession
    {

        #region Fields and Properties

        // TODO Remove currently not used
        public string Id { get; set; }
        public string TeamId { get; set; }
        public Team Team { get; set; }
        public string LeagueSessionScheduleId { get; set; }
        public LeagueSessionSchedule LeagueSessionSchedule { get; set; }

        #endregion
    }
}
