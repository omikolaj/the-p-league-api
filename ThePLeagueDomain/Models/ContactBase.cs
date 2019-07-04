using System;
using System.ComponentModel.DataAnnotations;

namespace ThePLeagueDomain.Models
{
  public enum PreferredContact
  {
    None = 0,
    Cell = 1,
    Email = 2
  }
  public class ContactBase
  {
    #region Fields and Properties    
    public long? Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    public PreferredContact PreferredContact { get; set; }

    #endregion
  }
}