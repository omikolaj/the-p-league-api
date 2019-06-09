using System.Collections.Generic;
using System.Linq;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.ViewModels;
using ThePLeagueDomain.ViewModels.Merchandise;

namespace ThePLeagueDomain.Converters
{
  public static class GearImageConverter
  {
    #region Methods
    public static GearImageViewModel Convert(GearImage gearImage)
    {
      GearImageViewModel gearImageViewModel = new GearImageViewModel();
      gearImageViewModel.Big = gearImage.Big;
      gearImageViewModel.Id = gearImage.Id;
      gearImageViewModel.Medium = gearImage.Medium;
      gearImageViewModel.Name = gearImage.Name;
      gearImageViewModel.Size = gearImage.Size;
      gearImageViewModel.Small = gearImage.Small;
      gearImageViewModel.Type = gearImage.Type;
      gearImageViewModel.Url = gearImage.Url;
      gearImageViewModel.CloudinaryId = gearImage.CloudinaryId;

      return gearImageViewModel;
    }

    public static List<GearImageViewModel> ConvertList(IEnumerable<GearImage> gearImages)
    {
      return gearImages.Select(gearImage =>
      {
        GearImageViewModel gearImageViewModel = new GearImageViewModel();
        gearImageViewModel.Big = gearImage.Big;
        gearImageViewModel.Id = gearImage.Id;
        gearImageViewModel.Medium = gearImage.Medium;
        gearImageViewModel.Name = gearImage.Name;
        gearImageViewModel.Size = gearImage.Size;
        gearImageViewModel.Small = gearImage.Small;
        gearImageViewModel.Type = gearImage.Type;
        gearImageViewModel.Url = gearImage.Url;
        gearImageViewModel.CloudinaryId = gearImage.CloudinaryId;
        return gearImageViewModel;
      }).ToList();
    }

    #endregion
  }
}