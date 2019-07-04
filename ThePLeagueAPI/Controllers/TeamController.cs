using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.EmailService;
using ThePLeagueAPI.Auth.Errors;
using ThePLeagueAPI.Filters;
using ThePLeagueDomain;
using ThePLeagueDomain.ViewModels.Team;

namespace ThePLeagueAPI.Controllers
{
  [Route("api/[controller]")]
  [Produces("application/json")]
  [ServiceFilter(typeof(ValidateModelStateAttribute))]
  public class TeamController : ThePLeagueBaseController
  {
    #region Fields and Properties
    private readonly IThePLeagueSupervisor _supervisor;
    private readonly ISendEmailService _emailService;

    #endregion

    #region Constructor
    public TeamController(IThePLeagueSupervisor supervisor, ISendEmailService emailService)
    {
      this._supervisor = supervisor;
      this._emailService = emailService;
    }

    #endregion

    #region Controllers

    [HttpPost("signup")]
    public async Task<ActionResult<TeamSignUpFormViewModel>> TeamSignUp(TeamSignUpFormViewModel newTeamSignUpForm, CancellationToken ct = default(CancellationToken))
    {
      if (!this._emailService.SendEmail(newTeamSignUpForm))
      {
        return BadRequest(Errors.AddErrorToModelState(ErrorCodes.SendEmail, ErrorDescriptions.SendEmailFailure, ModelState));
      }

      newTeamSignUpForm = await this._supervisor.AddTeamSignUpFormAsync(newTeamSignUpForm, ct);

      return newTeamSignUpForm;
    }

    #endregion
  }
}