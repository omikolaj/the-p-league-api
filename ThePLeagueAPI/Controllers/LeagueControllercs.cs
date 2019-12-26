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
    [Authorize]
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

        [HttpPost]
        public async Task<ActionResult<LeagueViewModel>> Create([FromBody]LeagueViewModel newLeague, CancellationToken ct = default(CancellationToken))
        {
            newLeague = await this._supervisor.AddLeagueAsync(newLeague, ct);

            if (newLeague == null)
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.SportTypeAdd, ErrorDescriptions.SportTypeAddFailure, ModelState));
            }

            return new JsonResult(newLeague);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<LeagueViewModel>> Update([FromBody]LeagueViewModel updatedLeague, CancellationToken ct = default(CancellationToken))
        {
            if (!await this._supervisor.UpdateLeagueAsync(updatedLeague, ct))
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.LeagueUpdate, ErrorDescriptions.LeagueUpdateFailure, ModelState));
            }

            updatedLeague = await this._supervisor.GetLeagueByIdAsync(updatedLeague.Id, ct);

            return new JsonResult(updatedLeague);
        }

        [HttpPatch]
        public async Task<ActionResult<LeagueViewModel>> BulkUpdate([FromBody]List<LeagueViewModel> updatedLeagues, CancellationToken ct = default(CancellationToken))
        {
            //if (!await this._supervisor.UpdateLeagueAsync(updatedLeague, ct))
            //{
            //    return BadRequest(Errors.AddErrorToModelState(ErrorCodes.LeagueUpdate, ErrorDescriptions.LeagueUpdateFailure, ModelState));
            //}

            //updatedLeague = await this._supervisor.GetLeagueByIdAsync(updatedLeague.Id, ct);

            return new JsonResult(updatedLeagues);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromBody]string id, CancellationToken ct = default(CancellationToken))
        {
            LeagueViewModel leagueToDelete = await this._supervisor.GetLeagueByIdAsync(id, ct);

            if (leagueToDelete == null)
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.LeagueDelete, ErrorDescriptions.LeagueNotFound, ModelState));
            }

            if (!await this._supervisor.DeleteLeagueAsync(id))
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.LeagueDelete, ErrorDescriptions.LeagueDeleteFailure, ModelState));
            }

            return new OkObjectResult(true);
        }

        [HttpDelete]
        public async Task<ActionResult<LeagueViewModel>> BulkDelete([FromBody]List<string> idsToDelete, CancellationToken ct = default(CancellationToken))
        {
            //if (!await this._supervisor.UpdateLeagueAsync(updatedLeague, ct))
            //{
            //    return BadRequest(Errors.AddErrorToModelState(ErrorCodes.LeagueUpdate, ErrorDescriptions.LeagueUpdateFailure, ModelState));
            //}

            //updatedLeague = await this._supervisor.GetLeagueByIdAsync(updatedLeague.Id, ct);

            return new JsonResult(idsToDelete);
        }

        #endregion
    }
}
