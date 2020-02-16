using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ThePLeagueDomain.ViewModels.Schedule
{
    public class SportTypeViewModel
    {
        #region Fields and Properties

        public string Id { get; set; }
        public bool Active { get; set; } = true;
        [Required]
        public string Name { get; set; }
        public ICollection<LeagueViewModel> Leagues { get; set; }

        #endregion
    }
}
