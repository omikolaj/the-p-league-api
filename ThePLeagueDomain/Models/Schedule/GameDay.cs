using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class GameDay
    {
        #region Fields and Properties
        public string Id { get; set; }
        [Required]
        public string GamesDay { get; set; }
        public IEnumerable<GameTime> GamesTimes { get; set; }

        #endregion
    }
}
