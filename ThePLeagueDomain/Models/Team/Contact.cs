using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThePLeagueDomain.Models.Team
{
  public class Contact : ContactBase
  {
    #region Fields and Properties
    public long? TeamSignUpFormId { get; set; }
    public virtual TeamSignUpForm TeamSignUpForm { get; set; }

    #endregion
  }
}