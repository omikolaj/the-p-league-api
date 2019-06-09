using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Repositories;
using ThePLeagueDomain.Repositories.Merchandise;
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

    #endregion

    #region Constructor
    public ThePLeagueSupervisor(
        IApplicationUserRepository applicationUserRepository,
        IGearItemRepository gearItemRepository,
        IGearImageRepository gearImageRepository,
        IGearSizeRepository gearSizeRepository
      )
    {
      this._applicationUserRepository = applicationUserRepository;
      this._gearItemRepository = gearItemRepository;
      this._gearImageRepository = gearImageRepository;
      this._gearSizeRepository = gearSizeRepository;
    }

    #endregion
  }
}