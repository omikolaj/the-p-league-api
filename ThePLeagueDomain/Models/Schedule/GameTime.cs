using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ThePLeagueDomain.Models.Schedule
{
    public class GameTime
    {
        public string Id { get; set; }
        [Required]
        public string GamesTime { get; set; }
    }
}
