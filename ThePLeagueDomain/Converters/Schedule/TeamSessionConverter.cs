using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueDomain.Converters.Schedule
{
    public class TeamSessionConverter
    {
        #region Methods

        public static TeamSessionViewModel Convert(TeamSession teamSession)
        {
            TeamSessionViewModel model = new TeamSessionViewModel()
            {
                Id = teamSession.Id,
                TeamId = teamSession.TeamId,
                Team = TeamConverter.Convert(teamSession.Team),
                LeagueSessionScheduleId = teamSession.LeagueSessionScheduleId,
                LeagueSessionSchedule = LeagueSessionScheduleConverter.Convert(teamSession.LeagueSessionSchedule)
            };

            return model;
        }

        public static List<TeamSessionViewModel> ConvertList(List<TeamSession> teamSessions)
        {
            return teamSessions.Select(teamSession =>
            {
                TeamSessionViewModel model = new TeamSessionViewModel()
                {
                    Id = teamSession.Id,
                    TeamId = teamSession.TeamId,
                    Team = TeamConverter.Convert(teamSession.Team),
                    LeagueSessionScheduleId = teamSession.LeagueSessionScheduleId,
                    LeagueSessionSchedule = LeagueSessionScheduleConverter.Convert(teamSession.LeagueSessionSchedule)
                };

                return model;

            }).ToList();
        }

        #endregion
    }
}
