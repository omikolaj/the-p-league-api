using System.Collections.Generic;
using System.Linq;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.ViewModels;
using ThePLeagueDomain.ViewModels.Merchandise;

namespace ThePLeagueDomain.Converters
{
  public static class GearSizeConverter
  {
    #region Methods
    public static GearSizeViewModel Convert(GearSize gearSize)
    {
      GearSizeViewModel gearSizeViewModel = new GearSizeViewModel();
      gearSizeViewModel.Available = gearSize.Available;
      gearSizeViewModel.Color = gearSize.Color;
      gearSizeViewModel.Id = gearSize.Id;
      gearSizeViewModel.Size = gearSize.Size;

      return gearSizeViewModel;
    }

    public static List<GearSizeViewModel> ConvertList(IEnumerable<GearSize> gearSizes)
    {
      return gearSizes.Select(gearSize =>
      {
        GearSizeViewModel gearSizeViewModel = new GearSizeViewModel();
        gearSizeViewModel.Available = gearSize.Available;
        gearSizeViewModel.Color = gearSize.Color;
        gearSizeViewModel.Id = gearSize.Id;
        gearSizeViewModel.Size = gearSize.Size;

        return gearSizeViewModel;
      }).ToList();
    }

    #endregion
  }
}