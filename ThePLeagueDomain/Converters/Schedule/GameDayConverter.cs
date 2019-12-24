using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueDomain.Converters.Schedule
{
    public class GameDayConverter
    {
        #region Methods

        public static GameDayViewModel Convert(GameDay gameDay)
        {
            GameDayViewModel model = new GameDayViewModel();
            model.GamesDay = gameDay.GamesDay;
            model.GamesTimes = GameTimeConverter.ConvertList(gameDay.GamesTimes);

            return model;
        }

        public static List<GameDayViewModel> ConvertList(IEnumerable<GameDay> gameDays)
        {
            return gameDays.Select(gameDay =>
            {
                GameDayViewModel model = new GameDayViewModel();
                model.GamesDay = gameDay.GamesDay;
                model.GamesTimes = GameTimeConverter.ConvertList(gameDay.GamesTimes);

                return model;
            }).ToList();
        }

        #endregion
    }
}
