using System.Collections.Generic;
using System.Linq;

namespace ThePLeagueDomain.ViewModels.Merchandise
{
  public class GearItemViewModel
  {
    public long? Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool InStock { get; set; }
    // public string FormData { get; set; }
    public IEnumerable<GearSizeViewModel> Sizes { get; set; }
    public IEnumerable<GearImageViewModel> Images { get; set; }
  }
}