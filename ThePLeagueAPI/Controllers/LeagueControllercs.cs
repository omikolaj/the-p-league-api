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
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueAPI.Controllers
{
    [Route("api/leagues")]
    [Produces("application/json")]
    [ServiceFilter(typeof(ValidateModelStateAttribute))]
    public class LeagueControllercs : ControllerBase
    {
        #region Properties and Fields

        private readonly IThePLeagueSupervisor _supervisor;

        #endregion

        #region Constructor

        public LeagueControllercs(IThePLeagueSupervisor supervisor)
        {
            this._supervisor = supervisor;
        }

        #endregion

        #region Controllers

        [HttpPost("new")]
        [Authorize]
        public async Task<ActionResult<LeagueViewModel>> Create([FromBody]LeagueViewModel newLeague, CancellationToken ct = default(CancellationToken))
        {
            newLeague = await this._supervisor.AddLeagueAsync(newLeague, ct);

            if (newLeague == null)
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.SportTypeAdd, ErrorDescriptions.SportTypeAddFailure, ModelState));
            }

            return new JsonResult(newLeague);
        }

        // currently not in use, only BulkUpdate is used
        //[HttpPatch("{id}")]
        //public async Task<ActionResult<LeagueViewModel>> Update([FromBody]LeagueViewModel updatedLeague, CancellationToken ct = default(CancellationToken))
        //{
        //    if (!await this._supervisor.UpdateLeagueAsync(updatedLeague, ct))
        //    {
        //        return BadRequest(Errors.AddErrorToModelState(ErrorCodes.LeagueUpdate, ErrorDescriptions.LeagueUpdateFailure, ModelState));
        //    }

        //    updatedLeague = await this._supervisor.GetLeagueByIdAsync(updatedLeague.Id, ct);

        //    return new JsonResult(updatedLeague);
        //}

        [HttpPatch]
        [Authorize]
        public async Task<ActionResult<LeagueViewModel>> UpdateLeagues([FromBody]List<LeagueViewModel> updatedLeagues, CancellationToken ct = default(CancellationToken))
        {
            if (!await this._supervisor.UpdateLeaguesAsync(updatedLeagues, ct))
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.LeagueUpdate, ErrorDescriptions.LeagueUpdateFailure, ModelState));
            }

            updatedLeagues = await this._supervisor.GetLeaguesByIdsAsync(updatedLeagues.Select(league => league.Id).ToList(), ct);

            return new JsonResult(updatedLeagues);
        }

        // currently not in use. Only bulkDelete is used by the client
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<bool>> Delete([FromBody]string id, CancellationToken ct = default(CancellationToken))
        //{
        //    LeagueViewModel leagueToDelete = await this._supervisor.GetLeagueByIdAsync(id, ct);

        //    if (leagueToDelete == null)
        //    {
        //        return BadRequest(Errors.AddErrorToModelState(ErrorCodes.LeagueDelete, ErrorDescriptions.LeagueNotFound, ModelState));
        //    }

        //    if (!await this._supervisor.DeleteLeagueAsync(id))
        //    {
        //        return BadRequest(Errors.AddErrorToModelState(ErrorCodes.LeagueDelete, ErrorDescriptions.LeagueDeleteFailure, ModelState));
        //    }

        //    return new OkObjectResult(true);
        //}

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<LeagueViewModel>> DeleteLeagues([FromBody]List<string> idsToDelete, CancellationToken ct = default(CancellationToken))
        {
            if (!await this._supervisor.DeleteLeaguesAsync(idsToDelete, ct))
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.LeagueDelete, ErrorDescriptions.LeagueDeleteFailure, ModelState));
            }

            return new OkObjectResult(true);
        }

        #region Session Schedules

        [HttpPost("sessions/active-sessions-info")]
        [Authorize]
        public async Task<ActionResult<List<ActiveSessionInfoViewModel>>> ActiveSchedulesInfo([FromBody]List<string> leagueIds, CancellationToken ct = default(CancellationToken))
        {
            List<ActiveSessionInfoViewModel> activeSessions = await this._supervisor.GetActiveSessionsInfoAsync(leagueIds, ct);
            
            if(activeSessions == null)
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

        #endregion

        #endregion
    }
}
