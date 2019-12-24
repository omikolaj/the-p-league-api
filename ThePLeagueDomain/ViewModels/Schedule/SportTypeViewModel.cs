﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ThePLeagueDomain.ViewModels.Schedule
{
    public class SportTypeViewModel
    {
        #region Fields and Properties

        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<LeagueViewModel> Leagues { get; set; }

        #endregion
    }

    public class TestViewModel
    {
        public string Name { get; set; }
    }
}