using Services.EmailService;
using ThePLeagueDomain.ViewModels.Merchandise;
using ThePLeagueDomain.ViewModels.Team;

namespace Services.EmailService
{
  public interface ISendEmailService
  {
    bool SendEmail(TeamSignUpFormViewModel email);
    bool SendEmail(PreOrderViewModel email, GearItemViewModel gearItemPreOrder);
  }
}