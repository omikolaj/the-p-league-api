using System;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Team;
using ThePLeagueDomain.ViewModels;
using ThePLeagueDomain.ViewModels.Team;

namespace ThePLeagueDomain.Converters.TeamConverters
{
  public static class ContactConverter
  {
    #region Methods
    public static ContactViewModel Convert(Contact contact)
    {
      ContactViewModel contactViewModel = new ContactViewModel();
      contactViewModel.Id = contact.Id;
      contactViewModel.TeamSignUpFormId = contact.TeamSignUpFormId;
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