using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ThePLeagueDomain.ViewModels.Merchandise
{
  public class GearItemViewModel
  {
    public long? Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool InStock { get; set; }
    // public string FormData { get; set; }
    public IList<GearSizeViewModel> Sizes { get; set; }
    public IList<GearImageViewModel> Images { get; set; } = new List<GearImageViewModel>();
    public IList<IFormFile> gearImages { get; set; }
    public IList<GearImageViewModel> NewImages { get; set; }
  }

}