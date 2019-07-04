using System;
using System.ComponentModel.DataAnnotations;
using ThePLeagueDomain.Models;

namespace ThePLeagueDomain.ViewModels
{
  public class ContactBaseViewModel
  {
    #region Fields and Properties    
    public long? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public PreferredContact PreferredContact { get; set; }

    #endregion
  }
}