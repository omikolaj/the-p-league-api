using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueDomain.Converters.Schedule
{
    public class LeagueSessionScheduleConverter
    {
        #region Methods

        public static LeagueSessionScheduleViewModel Convert(LeagueSessionSchedule sessionSchedule)
        {
            LeagueSessionScheduleViewModel model = new LeagueSessionScheduleViewModel();
            model.Active = sessionSchedule.Active;
            model.ByeWeeks = sessionSchedule.ByeWeeks;
            model.GamesDays = GameDayConverter.ConvertList(sessionSchedule.GamesDays);
            model.Id = sessionSchedule.Id;
            model.LeagueID = sessionSchedule.LeagueID;
            model.Matches = MatchConverter.ConvertList(sessionSchedule.Matches);
            model.NumberOfWeeks = sessionSchedule.NumberOfWeeks;
            model.SessionEnd = sessionSchedule.SessionEnd;
            model.SessionStart = sessionSchedule.SessionStart;
            model.Teams = TeamConverter.ConvertList(sessionSchedule.Teams);

            return model;
        }

        public static List<LeagueSessionScheduleViewModel> ConvertList(IEnumerable<LeagueSessionSchedule> sessionSchedules)
        {
            return sessionSchedules.Select(sessionSchedule => 
            {
                LeagueSessionScheduleViewModel model = new LeagueSessionScheduleViewModel();
                model.Active = sessionSchedule.Active;
                model.ByeWeeks = sessionSchedule.ByeWeeks;
                model.GamesDays = GameDayConverter.ConvertList(sessionSchedule.GamesDays);
                model.Id = sessionSchedule.Id;
                model.LeagueID = sessionSchedule.LeagueID;
                model.Matches = MatchConverter.ConvertList(sessionSchedule.Matches);
                model.NumberOfWeeks = sessionSchedule.NumberOfWeeks;
                model.SessionEnd = sessionSchedule.SessionEnd;
                model.SessionStart = sessionSchedule.SessionStart;
                model.Teams = TeamConverter.ConvertList(sessionSchedule.Teams);

                return model;
            }).Tolist();
        }

        #endregion
    }
}
