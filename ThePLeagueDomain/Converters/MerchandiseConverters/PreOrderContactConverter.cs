using System;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.Models.TeamSignUp;
using ThePLeagueDomain.ViewModels;
using ThePLeagueDomain.ViewModels.Team;

namespace ThePLeagueDomain.Converters.MerchandiseConverters
{
  public static class PreOrderContactConverter
  {
    #region Methods
    public static PreOrderContactViewModel Convert(PreOrderContact contact)
    {
      PreOrderContactViewModel contactViewModel = new PreOrderContactViewModel();
      contactViewModel.Id = contact.Id;
      contactViewModel.PreOrderId = contact.PreOrderId;
      contactViewModel.Email = contact.Email;
      contactViewModel.FirstName = contact.FirstName;
      contactViewModel.LastName = contact.LastName;
      contactViewModel.PhoneNumber = contact.PhoneNumber;
      contactViewModel.PreferredContact = contact.PreferredContact;


      return contactViewModel;
    }
    #endregion
  }
}