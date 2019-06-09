using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Converters;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels;
using ThePLeagueDomain.ViewModels.Merchandise;

namespace ThePLeagueDomain.Supervisor
{
  public partial class ThePLeagueSupervisor : IThePLeagueSupervisor
  {

    #region Methods
    public async Task<ApplicationUserViewModel> AddUserAsync(ApplicationUserViewModel userViewModel, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public async Task<bool> DeleteUserAsync(string id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public async Task<List<ApplicationUserViewModel>> GetAllUsersAsync(CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    public async Task<ApplicationUserViewModel> GetUserByIDAsync(string id, CancellationToken ct = default)
    {
      ApplicationUserViewModel userViewModel = ApplicationUserConverter.Convert(await _applicationUserRepository.GetByIDAsync(id, ct));

      // Retrieve navigational properties here if necessary
      // userViewModel.Comments = await GetAllCommentsByUserIdAsync(id, ct);

      return userViewModel;
    }

    public async Task<bool> UpdateUserAsync(ApplicationUserViewModel userViewModel, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    #endregion
  }
}