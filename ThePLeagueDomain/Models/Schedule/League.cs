using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThePLeagueDomain.Models.Schedule
{
    public class League
    {
        #region Fields and Properties

        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public bool Selected { get; set; }        
        public string SportTypeID { get; set; }
        public SportType SportType { get; set; }
        public IEnumerable<LeagueSessionSchedule> Sessions { get; set; }

        #endregion
    }
}
