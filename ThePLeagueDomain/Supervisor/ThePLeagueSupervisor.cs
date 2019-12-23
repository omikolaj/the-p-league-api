using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Repositories;
using ThePLeagueDomain.Repositories.Gallery;
using ThePLeagueDomain.Repositories.Merchandise;
using ThePLeagueDomain.Repositories.TeamSignUp;
using ThePLeagueDomain.ViewModels;

namespace ThePLeagueDomain.Supervisor
{
  public partial class ThePLeagueSupervisor : IThePLeagueSupervisor
  {
    #region Properties and Fields
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IGearItemRepository _gearItemRepository;
    private readonly IGearImageRepository _gearImageRepository;
    private readonly IGearSizeRepository _gearSizeRepository;
    private readonly ILeagueImageRepository _leagueImageRepository;
    private readonly ITeamSignUpRepository _teamSignUpRepository;
    private readonly IPreOrderRepository _preOrderRepository;

    #endregion

    #region Constructor
    public ThePLeagueSupervisor(
        IApplicationUserRepository applicationUserRepository,
        IGearItemRepository gearItemRepository,
        IGearImageRepository gearImageRepository,
        IGearSizeRepository gearSizeRepository,
        ILeagueImageRepository leagueImageRepository,
        ITeamSignUpRepository teamSignUpRepository,
        IPreOrderRepository preOrderRepository
      )
    {
      this._applicationUserRepository = applicationUserRepository;
      this._gearItemRepository = gearItemRepository;
      this._gearImageRepository = gearImageRepository;
      this._gearSizeRepository = gearSizeRepository;
      this._leagueImageRepository = leagueImageRepository;
      this._teamSignUpRepository = teamSignUpRepository;
      this._preOrderRepository = preOrderRepository;
    }

    #endregion
  }
}