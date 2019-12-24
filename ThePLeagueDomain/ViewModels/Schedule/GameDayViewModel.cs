using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.ViewModels.Schedule
{
    public class GameDayViewModel
    {
        #region Fields and Properties

        public string Id { get; set; }
        public string GamesDay { get; set; }
        public IEnumerable<GameTimeViewModel> GamesTimes { get; set; }

        #endregion
    }
}
