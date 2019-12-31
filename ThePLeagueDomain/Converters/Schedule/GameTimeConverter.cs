using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueDomain.Converters.Schedule
{
    public class GameTimeConverter
    {
        #region Methods

        public static GameTimeViewModel Convert(GameTime gameTime)
        {
            GameTimeViewModel model = new GameTimeViewModel();
            model.GamesTime = (Int64)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
            model.GameDayId = gameTime.GameDayId;
            model.Id = gameTime.Id;

            return model;
        }

        public static List<GameTimeViewModel> ConvertList(IEnumerable<GameTime> gameTimes)
        {
            return gameTimes.Select(gameTime =>
            {
                GameTimeViewModel model = new GameTimeViewModel();
                model.GamesTime = (Int64)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
                model.Id = gameTime.Id;
                model.GameDayId = gameTime.GameDayId;

                return model;
            }).ToList();
        }

        #endregion
    }
}
