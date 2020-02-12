using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueAPI.Auth.Errors;
using ThePLeagueAPI.Filters;
using ThePLeagueDomain;
using ThePLeagueDomain.Supervisor;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ServiceFilter(typeof(ValidateModelStateAttribute))]
    public class SchedulesController : ControllerBase
    {
        #region Fields and Properties

        private readonly IThePLeagueSupervisor _supervisor;

        #endregion

        #region Constructor

        public SchedulesController(IThePLeagueSupervisor supervisor)
        {
            this._supervisor = supervisor;
        }

        #endregion

        #region Controllers

        [HttpPost("sessions/active-sessions-info")]
        [Authorize]
        [ResponseCache(CacheProfileName = "OneHour")]
        public async Task<ActionResult<List<ActiveSessionInfoViewModel>>> ActiveSchedulesInfo([FromBody]List<string> leagueIds, CancellationToken ct = default(CancellationToken))
        {
            List<ActiveSessionInfoViewModel> activeSessions = await this._supervisor.GetActiveSessionsInfoAsync(leagueIds, ct);

            if (activeSessions == null)
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.ActiveSessionsInfo, ErrorDescriptions.ActiveSessionsInfoFailure, ModelState));
            }

            return new JsonResult(activeSessions);
        }

        [HttpPost("sessions")]
        [Authorize]
        public async Task<ActionResult<bool>> PublishLeagueSessionSchedules([FromBody]List<LeagueSessionScheduleViewModel> newSessionSchedules, CancellationToken ct = default(CancellationToken))
        {
            if (!await this._supervisor.PublishSessionsSchedulesAsync(newSessionSchedules, ct))
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.PublishNewSession, ErrorDescriptions.PublishingNewSessionsFailure, ModelState));
            }

            return new JsonResult(true);
        }

        [HttpGet("sessions")]
        [ResponseCache(CacheProfileName = "OneHour")]
        public async Task<ActionResult<List<LeagueSessionScheduleViewModel>>> GetAllActiveSessionSchedules(CancellationToken ct = default(CancellationToken))
        {
            List<LeagueSessionScheduleViewModel> sessions = await this._supervisor.GetAllActiveSessionsAsync(ct);

            return new JsonResult(sessions);
        }

        [HttpPost("sessions/{id}/matches/{matchId}/report")]
        [Authorize]
        public async Task<ActionResult<MatchResultViewModel>> ReportMatch([FromBody]MatchResultViewModel reportMatch, CancellationToken ct = default(CancellationToken))
        {
            reportMatch = await this._supervisor.ReportMatchAsync(reportMatch, ct);
            if (reportMatch == null)
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.MatchResult, ErrorDescriptions.MatchResulFailure, ModelState));
            }

            return new JsonResult(reportMatch);
        }

        #endregion
    }
}
