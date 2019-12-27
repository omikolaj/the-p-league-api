using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.EmailService;
using ThePLeagueAPI.Auth.Errors;
using ThePLeagueAPI.Filters;
using ThePLeagueDomain;
using ThePLeagueDomain.ViewModels.Schedule;
using ThePLeagueDomain.ViewModels.Team;

namespace ThePLeagueAPI.Controllers
{
    [Route("api/teams")]
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

        #region Team Sign Up

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

        #region Teams

        [HttpPost("assign")]
        [Authorize]
        public async Task<ActionResult<List<TeamViewModel>>> AssignTeams(List<TeamViewModel> teamsWithAssignedLeague, CancellationToken ct = default(CancellationToken))
        {
            teamsWithAssignedLeague = await this._supervisor.AssignTeamsAsync(teamsWithAssignedLeague);

            if(teamsWithAssignedLeague == null)
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.TeamAssign, ErrorDescriptions.TeamAssign, ModelState));
            }

            return new JsonResult(teamsWithAssignedLeague);

        }

        [HttpPost("unassign")]
        [Authorize]
        public async Task<ActionResult<List<string>>> UnassignTeams(List<string> teamsToUnassignFromLeague, CancellationToken ct = default(CancellationToken))
        {
            teamsToUnassignFromLeague = await this._supervisor.UnassignTeamsAsync(teamsToUnassignFromLeague);

            if (teamsToUnassignFromLeague == null)
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.TeamUnassign, ErrorDescriptions.TeamUnassign, ModelState));
            }

            return new JsonResult(teamsToUnassignFromLeague);
        }

        [HttpPost("new")]
        [Authorize]
        public async Task<ActionResult<TeamViewModel>> CreateTeam([FromBody]TeamViewModel newTeam, CancellationToken ct = default(CancellationToken))
        {
            newTeam = await this._supervisor.AddTeamAsync(newTeam, ct);

            if (newTeam == null)
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.TeamAdd, ErrorDescriptions.TeamAddFailure, ModelState));
            }

            return new JsonResult(newTeam);
        }

        [HttpPatch("update")]
        [Authorize]
        public async Task<ActionResult<TeamViewModel>> UpdateTeams([FromBody]List<TeamViewModel> updatedTeams, CancellationToken ct = default(CancellationToken))
        {
            if(!await this._supervisor.UpdateTeamsAsync(updatedTeams, ct))
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.TeamUpdate, ErrorDescriptions.TeamUpdateFailure, ModelState));
            }

            updatedTeams = await this._supervisor.GetTeamsByIdsAsync(updatedTeams.Select(team => team.Id).ToList());

            return new JsonResult(updatedTeams);
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteTeams([FromBody]List<string> idsToDelete, CancellationToken ct = default(CancellationToken))
        {
            if(!await this._supervisor.DeleteTeamsAsync(idsToDelete, ct))
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.TeamDelete, ErrorDescriptions.TeamDeleteFailure, ModelState));
            }

            return new OkObjectResult(true);
        }

        #endregion

        #endregion
    }
}