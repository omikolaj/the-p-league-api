using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueDomain.Converters.Schedule
{
    public class LeagueConverter
    {
        #region Methods

        public static LeagueViewModel Convert(League league)
        {
            LeagueViewModel model = new LeagueViewModel();
            model.Id = league.Id;
            model.Selected = league.Selected;
            model.Sessions = LeagueSessionScheduleConverter.ConvertList(league?.Sessions);
            model.SportTypeID = league.SportTypeID;
            model.Teams = TeamConverter.ConvertList(league.Teams);
            model.Type = league.Type;

            return model;
        }

        public static List<LeagueViewModel> ConvertList(IEnumerable<League> leagues)
        {
            return leagues.Select(league => 
            {
                LeagueViewModel model = new LeagueViewModel();
                model.Id = league.Id;
                model.Selected = league.Selected;
                model.Sessions = LeagueSessionScheduleConverter.ConvertList(league?.Sessions);
                model.SportTypeID = league.SportTypeID;
                model.Teams = TeamConverter.ConvertList(league.Teams);
                model.Type = league.Type;

                return model;
            }).ToList()
        }

        #endregion
    }
}
