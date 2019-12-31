using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class GameDay
    {
        #region Fields and Properties
        public string Id { get; set; }        
        public string GamesDay { get; set; }
        public string LeagueSessionScheduleId { get; set; }
        public ICollection<GameTime> GamesTimes { get; set; } = new Collection<GameTime>();

        #endregion
    }
}
