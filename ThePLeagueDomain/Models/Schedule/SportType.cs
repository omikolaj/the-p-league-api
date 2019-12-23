using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class SportType
    {
        #region Fields and Properties

        public string Id { get; set; }
        public string Name { get; set; }
        public  IEnumerable<League> Leagues { get; set; }

        #endregion
    }
}
