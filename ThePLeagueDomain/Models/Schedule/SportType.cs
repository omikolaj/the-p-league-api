using System;
using System.Collections.Generic;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class SportType
    {
        #region Fields and Properties

        public long? ID { get; set; }
        public string Name { get; set; }
        public  IEnumerable<League> Leagues { get; set; }

        #endregion
    }
}
