using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ThePLeagueDomain.Models.Merchandise
{
  public class GearItem
  {
    #region Fields and Properties

    public long? Id { get; set; }
    public string Name { get; set; }
    [Column(TypeName = "Money")]
    public decimal Price { get; set; }
    public bool InStock { get; set; }
    public IEnumerable<GearSize> Sizes { get; set; }
    public IEnumerable<GearImage> Images { get; set; }

    #endregion
  }
}