﻿using System;
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
            model.Active = league.Active;
            model.SportTypeID = league.SportTypeID;
            model.Teams = league.Teams == null ? null : TeamConverter.ConvertList(league.Teams);
            model.Type = league.Type;
            model.Name = league.Name;

            return model;
        }

        public static List<LeagueViewModel> ConvertList(IEnumerable<League> leagues)
        {
            return leagues.Select(league =>
            {
                LeagueViewModel model = new LeagueViewModel();
                model.Id = league.Id;
                model.Selected = league.Selected;
                model.Active = league.Active;
                model.SportTypeID = league.SportTypeID;
                model.Teams = league.Teams == null ? null : TeamConverter.ConvertList(league.Teams);
                model.Type = league.Type;
                model.Name = league.Name;

                return model;
            }).ToList();
        }

        #endregion
    }
}
