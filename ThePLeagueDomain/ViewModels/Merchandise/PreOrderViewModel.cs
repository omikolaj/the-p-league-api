using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.ViewModels.Team;

namespace ThePLeagueDomain.ViewModels.Merchandise
{
  public class PreOrderViewModel
  {
    #region Fields and Properties
    public long? Id { get; set; }
    public long? GearItemId { get; set; }
    public long? Quantity { get; set; }
    public Size Size { get; set; }
    public virtual PreOrderContactViewModel Contact { get; set; }

    #endregion
  }
}