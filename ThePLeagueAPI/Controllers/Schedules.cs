using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueAPI.Filters;
using ThePLeagueDomain;
using ThePLeagueDomain.Supervisor;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ServiceFilter(typeof(ValidateModelStateAttribute))]
    public class Schedules : ControllerBase
    {
        #region Fields and Properties

        private readonly IThePLeagueSupervisor _supervisor;

        #endregion

        #region Constructor

        public Schedules(IThePLeagueSupervisor supervisor)
        {
            this._supervisor = supervisor;
        }

        #endregion

        #region Controllers

        

        #endregion
    }
}
