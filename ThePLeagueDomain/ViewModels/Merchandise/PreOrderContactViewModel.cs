using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.ViewModels;

namespace ThePLeagueDomain.Models.Merchandise
{
  public class PreOrderContactViewModel : ContactBaseViewModel
  {
    #region Fields and Properties    
    public long? PreOrderId { get; set; }
    public virtual PreOrder PreOrder { get; set; }

    #endregion
  }
}