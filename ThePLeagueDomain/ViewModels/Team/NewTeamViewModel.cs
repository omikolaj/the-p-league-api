using ThePLeagueDomain.Models.Team;

namespace ThePLeagueDomain.ViewModels.Team
{
  public class TeamSignUpFormViewModel
  {
    #region Fields and Properties

    public long? Id { get; set; }
    public string Name { get; set; }
    public ContactViewModel Contact { get; set; }

    #endregion
  }
}