using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ThePLeagueDomain.Models.Merchandise
{
  public enum Size
  {
    NONE = 0,
    XS = 1,
    S = 2,
    M = 3,
    L = 4,
    XL = 5,
    XXL = 6,
    ALL = 10
  }
  public class GearSize
  {
    #region Fields and Properties        
    public long? Id { get; set; }
    public long? GearItemId { get; set; }
    public GearItem GearItem { get; set; }
    public Size Size { get; set; }
    public bool Available { get; set; }
    public string Color { get; set; }

    #endregion
  }
}