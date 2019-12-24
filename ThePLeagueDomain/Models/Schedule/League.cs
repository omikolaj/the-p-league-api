using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThePLeagueDomain.Models.Schedule
{
    public class League
    {
        #region Fields and Properties

        public string Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public bool Selected { get; set; }
        [Required]
        public string SportTypeID { get; set; }
        public IEnumerable<LeagueSessionSchedule> Sessions { get; set; }

        #endregion
    }
}
