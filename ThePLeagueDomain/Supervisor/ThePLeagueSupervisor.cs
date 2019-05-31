using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Repositories;
using ThePLeagueDomain.ViewModels;

namespace ThePLeagueDomain.Supervisor
{
  public partial class ThePLeagueSupervisor : IThePLeagueSupervisor
  {
    #region Properties and Fields
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IApplicationUserRepository _applicationUserRepository;

    #endregion

    #region Constructor
    public ThePLeagueSupervisor(IRefreshTokenRepository refreshTokenRepository, IApplicationUserRepository applicationUser)
    {
      this._refreshTokenRepository = refreshTokenRepository;
      this._applicationUserRepository = applicationUser;
    }

    #endregion
  }
}