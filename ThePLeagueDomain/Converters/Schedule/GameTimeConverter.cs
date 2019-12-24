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
            model.GamesTime = gameTime.GamesTime;
            model.Id = gameTime.Id;

            return model;
        }

        public static List<GameTimeViewModel> ConvertList(IEnumerable<GameTime> gameTimes)
        {
            return gameTimes.Select(gameTime =>
            {
                GameTimeViewModel model = new GameTimeViewModel();
                model.GamesTime = gameTime.GamesTime;
                model.Id = gameTime.Id;

                return model;
            }).ToList();
        }

        #endregion
    }
}
