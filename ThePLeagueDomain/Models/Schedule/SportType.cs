﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class SportType
    {
        #region Fields and Properties

        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public  IEnumerable<League> Leagues { get; set; }

        #endregion
    }
}
