using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueDomain.Converters.Schedule
{
    public class ActiveSessionInfoConverter
    {
        #region Methods

        public static ActiveSessionInfoViewModel Convert(ActiveSessionInfo sessionInfo)
        {
            ActiveSessionInfoViewModel model = new ActiveSessionInfoViewModel()
            {
                SessionId = sessionInfo.Id,
                LeagueId = sessionInfo.LeagueId,
                StartDate = sessionInfo.StartDate,
                EndDate = sessionInfo.EndDate
            };

            return model;

        } 

        public static List<ActiveSessionInfoViewModel> ConvertList(List<ActiveSessionInfo> sessionsInfo)
        {
            return sessionsInfo.Select(sessionInfo => 
            {
                ActiveSessionInfoViewModel model = new ActiveSessionInfoViewModel()
                {
                    SessionId = sessionInfo.Id,
                    LeagueId = sessionInfo.LeagueId,
                    StartDate = sessionInfo.StartDate,
                    EndDate = sessionInfo.EndDate
                };

                return model;
            }).ToList();
        }

        #endregion
    }
}
