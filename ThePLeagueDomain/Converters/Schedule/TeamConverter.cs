using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueDomain.Converters.Schedule
{
    public class TeamConverter
    {
        #region Methods

        public static TeamViewModel Convert(Team team)
        {
            TeamViewModel model = new TeamViewModel();
            model.Id = team.TeamId;
            model.Active = team.Active;
            model.LeagueID = team.LeagueID;            
            model.Name = team.Name;
            model.Selected = team.Selected;
            model.Active = team.Active;

            return model;
        }

        public static List<TeamViewModel> ConvertList(IEnumerable<Team> teams)
        {
            return teams.Select(team => 
            {
                TeamViewModel model = new TeamViewModel();
                model.Id = team.TeamId;
                model.Active = team.Active;
                model.LeagueID = team.LeagueID;                
                model.Name = team.Name;
                model.Selected = team.Selected;
                model.Active = team.Active;

                return model;
            }).ToList();
        }

        #endregion
    }
}
