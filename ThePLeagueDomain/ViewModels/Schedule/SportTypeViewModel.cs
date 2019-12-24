using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.ViewModels.Schedule
{
    public class SportTypeViewModel
    {
        #region Fields and Properties

        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<LeagueViewModel> Leagues { get; set; }

        #endregion
    }
}
