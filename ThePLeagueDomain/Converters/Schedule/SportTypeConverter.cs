using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueDomain.Converters.Schedule
{
    public class SportTypeConverter
    {
        #region Methods

        public static SportTypeViewModel Convert(SportType sportType)
        {
            SportTypeViewModel model = new SportTypeViewModel();
            model.Id = sportType.Id;
            model.Leagues = sportType.Leagues == null ? null : LeagueConverter.ConvertList(sportType.Leagues);
            model.Name = sportType.Name;
            model.Active = sportType.Active;

            return model;
        }

        public static List<SportTypeViewModel> ConvertList(IEnumerable<SportType> sportTypes)
        {
            return sportTypes.Select(sportType =>
            {
                SportTypeViewModel model = new SportTypeViewModel();
                model.Id = sportType.Id;
                model.Leagues = sportType.Leagues == null ? null : LeagueConverter.ConvertList(sportType.Leagues);
                model.Name = sportType.Name;
                model.Active = sportType.Active;

                return model;
            }).ToList();
        }

        #endregion
    }
}
