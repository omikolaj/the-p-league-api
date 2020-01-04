using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class GameTime
    {
        #region Fields and Properties

        public string Id { get; set; }
        public string GameDayId { get; set; }
        public DateTime GamesTime { get; set; }

        #endregion
    }
}
