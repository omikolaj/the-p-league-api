namespace ThePLeagueDomain.ViewModels.Team
{
  public class ContactViewModel : ContactBaseViewModel
  {
    #region Fields and Properties
    public long? TeamSignUpFormId { get; set; }
    public TeamSignUpFormViewModel TeamSignUpForm { get; set; }

    #endregion
  }
}