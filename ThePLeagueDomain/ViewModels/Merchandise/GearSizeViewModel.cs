using System.Collections.Generic;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Merchandise;

namespace ThePLeagueDomain.ViewModels.Merchandise
{
  public class GearSizeViewModel
  {
    #region Fields and Properties
    public long? Id { get; set; }
    public long? GearItemId { get; set; }
    public Size Size { get; set; }
    public bool Available { get; set; }
    public string Color { get; set; }

    #endregion
  }
}