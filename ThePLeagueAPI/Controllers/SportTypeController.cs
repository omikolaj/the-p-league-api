﻿using Microsoft.AspNetCore.Authorization;
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
    [Route("api/sport-types")]
    [Produces("application/json")]
    [ServiceFilter(typeof(ValidateModelStateAttribute))]
    public class SportTypeController : ControllerBase
    {
        #region Fields

        private readonly IThePLeagueSupervisor _supervisor;

        #endregion

        #region Constructor

        public SportTypeController(IThePLeagueSupervisor supervisor)
        {
            this._supervisor = supervisor;
        }

        #endregion

        #region Controllers
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<SportTypeViewModel>> GetAll(CancellationToken ct = default(CancellationToken))
        {
            List<SportTypeViewModel> sportTypes = await this._supervisor.GetAllSportTypesAsync(ct);

            return new JsonResult(sportTypes);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<SportTypeViewModel>> Create([FromBody]SportTypeViewModel newSportType, CancellationToken ct = default(CancellationToken))
        {
            newSportType = await this._supervisor.AddSportTypeAsync(newSportType, ct);

            if(newSportType == null)
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.SportTypeAdd, ErrorDescriptions.SportTypeAddFailure, ModelState));
            }

            return new JsonResult(newSportType);            
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<ActionResult<SportTypeViewModel>> Update([FromBody]SportTypeViewModel updatedSportType, CancellationToken ct = default(CancellationToken))
        {
            if(!await this._supervisor.UpdateSportTypeAsync(updatedSportType, ct))
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.SportTypeUpdate, ErrorDescriptions.SportTypeUpdateFailure, ModelState));
            }

            updatedSportType = await this._supervisor.GetSportTypeByIdAsync(updatedSportType.Id, ct);

            return new JsonResult(updatedSportType);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> Delete(string id, CancellationToken ct = default(CancellationToken))
        {
            if(!await this._supervisor.DeleteSportTypeAsync(id))
            {
                return BadRequest(Errors.AddErrorToModelState(ErrorCodes.SportTypeDelete, ErrorDescriptions.SportTypeDeleteFailure, ModelState));
            }

            return new OkObjectResult(true);
        }

        #endregion
    }
}
