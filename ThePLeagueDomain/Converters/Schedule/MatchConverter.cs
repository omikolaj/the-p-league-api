using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueDomain.Converters.Schedule
{
    public class MatchConverter
    {
        #region Methods

        public static MatchViewModel Convert(Match match)
        {
            MatchViewModel model = new MatchViewModel();
            model.AwayTeam = TeamConverter.Convert(match.AwayTeam);
            model.AwayTeamId = match.AwayTeamId;
            model.HomeTeam = TeamConverter.Convert(match.HomeTeam);
            model.HomeTeamId = match.HomeTeamId;
            model.Id = match.Id;
            model.LeagueID = match.LeagueID;
            model.LeagueSessionScheduleId = match.LeagueSessionScheduleId;
            model.League = LeagueConverter.Convert(match.League);

            return model;
        }

        public static List<MatchViewModel> ConvertList(IEnumerable<Match> matches)
        {
            return matches.Select(match => 
            {
                MatchViewModel model = new MatchViewModel();
                model.AwayTeam = TeamConverter.Convert(match.AwayTeam);
                model.HomeTeam = TeamConverter.Convert(match.HomeTeam);
                model.Id = match.Id;
                model.LeagueID = match.LeagueID;
                model.LeagueSessionScheduleId = match.LeagueSessionScheduleId;
                model.League = LeagueConverter.Convert(match.League);

                return model;

            }).ToList();
        }

        #endregion
    }
}
