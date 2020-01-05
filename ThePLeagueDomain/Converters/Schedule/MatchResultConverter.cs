using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueDomain.Converters.Schedule
{
    public class MatchResultConverter
    {
        #region Methods

        public static MatchResultViewModel Convert(MatchResult match)
        {
            return new MatchResultViewModel
            {
                Id = match.MatchId,
                MatchId = match.MatchId,
                Status = match.Status,
                AwayTeamScore = match.AwayTeamScore,
                AwayTeamId = match.AwayTeamId,
                HomeTeamScore = match.HomeTeamScore,
                HomeTeamId = match.HomeTeamId,
                Score = match.Score,
                WonTeamName = match.WonTeamName,
                LostTeamName = match.LostTeamName,
                LeagueId = match.LeagueId
            };
        }

        public static List<MatchResultViewModel> ConvertList(IEnumerable<MatchResult> matches)
        {
            return matches.Select(match => 
            {
                return new MatchResultViewModel
                {
                    Id = match.MatchId,
                    MatchId = match.MatchId,
                    Status = match.Status,
                    AwayTeamScore = match.AwayTeamScore,
                    AwayTeamId = match.AwayTeamId,
                    HomeTeamScore = match.HomeTeamScore,
                    HomeTeamId = match.HomeTeamId,
                    Score = match.Score,
                    WonTeamName = match.WonTeamName,
                    LostTeamName = match.LostTeamName,
                    LeagueId = match.LeagueId
                };
            }).ToList();
        }

        #endregion
    }
}
