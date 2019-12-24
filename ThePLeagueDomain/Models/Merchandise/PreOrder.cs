using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using ThePLeagueDomain.Models.TeamSignUp;

namespace ThePLeagueDomain.Models.Merchandise
{
  public class PreOrder
  {
    #region Fields and Properties
    public long? Id { get; set; }
    public long? GearItemId { get; set; }
    public long? Quantity { get; set; }
    public Size Size { get; set; }
    public virtual PreOrderContact Contact { get; set; }

    #endregion
  }
}