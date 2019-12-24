using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ThePLeagueDomain.Models.TeamSignUp
{
  public class TeamSignUpForm
  {
    #region Fields and Properties

    public long? Id { get; set; }
    [Required]
    public string Name { get; set; }
    public Contact Contact { get; set; }

    #endregion
  }
}