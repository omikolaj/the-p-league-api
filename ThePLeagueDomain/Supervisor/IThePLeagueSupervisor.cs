using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels;
using ThePLeagueDomain.ViewModels.Merchandise;

namespace ThePLeagueDomain
{
  public interface IThePLeagueSupervisor
  {
    #region RefreshToken
    // Task<bool> SaveRefreshTokenAsync(ApplicationUserViewModel userViewModel, string refreshToken, CancellationToken ct = default(CancellationToken));
    // Task<bool> DeleteRefreshTokenAsync(ApplicationUserViewModel userViewModel, RefreshToken refreshToken, CancellationToken ct = default(CancellationToken));
    // Task<bool> ValidateRefreshTokenAsync(ApplicationUserViewModel userViewModel, RefreshToken refreshToken, CancellationToken ct = default(CancellationToken));

    #endregion

    #region ApplicationUser
    Task<List<ApplicationUserViewModel>> GetAllUsersAsync(CancellationToken ct = default(CancellationToken));
    Task<ApplicationUserViewModel> GetUserByIDAsync(string ID, CancellationToken ct = default(CancellationToken));
    Task<ApplicationUserViewModel> AddUserAsync(ApplicationUserViewModel userViewModel, CancellationToken ct = default(CancellationToken));
    Task<bool> UpdateUserAsync(ApplicationUserViewModel userViewModel, CancellationToken ct = default(CancellationToken));
    Task<bool> DeleteUserAsync(string ID, CancellationToken ct = default(CancellationToken));

    #endregion 

    #region GearItem
    Task<List<GearItemViewModel>> GetAllGearItemsAsync(CancellationToken ct = default(CancellationToken));
    Task<GearItemViewModel> GetGearItemByIdAsync(long? gearItemId, CancellationToken ct = default(CancellationToken));
    Task<GearItemViewModel> AddGearItemAsync(GearItemViewModel gearItemViewModel, CancellationToken ct = default(CancellationToken));
    Task<bool> UpdateGearItemAsync(GearItemViewModel gearItemViewModel, CancellationToken ct = default(CancellationToken));
    Task<bool> DeleteGearItemAsync(long? gearItemId, CancellationToken ct = default(CancellationToken));

    #endregion

    #region GearSize
    Task<List<GearSizeViewModel>> GetAllGearSizesByGearItemIdAsync(long? gearItemId, CancellationToken ct = default(CancellationToken));
    Task<GearSizeViewModel> GetGearSizeByIdAsync(long id, CancellationToken ct = default(CancellationToken));
    Task<GearSizeViewModel> AddGearSizeAsync(GearSizeViewModel gearSizeViewModel, CancellationToken ct = default(CancellationToken));
    Task<bool> UpdateGearSizeAsync(GearSizeViewModel gearSizeViewModel, CancellationToken ct = default(CancellationToken));
    Task<bool> DeleteGearSizeAsync(long gearSizeId, CancellationToken ct = default(CancellationToken));

    #endregion

    #region GearImage    
    Task<List<GearImageViewModel>> GetAllGearImagesByGearItemIdAsync(long? gearItemId, CancellationToken ct = default(CancellationToken));
    Task<GearImageViewModel> AddGearImageAsync(GearImageViewModel gearSizeViewModel, CancellationToken ct = default(CancellationToken));
    Task<bool> UpdateGearImageAsync(GearImageViewModel gearSizeViewModel, CancellationToken ct = default(CancellationToken));
    Task<bool> DeleteGearImageAsync(long? gearImageId, CancellationToken ct = default(CancellationToken));

    #endregion

    #region Gallery
    Task<List<LeagueImageViewModel>> AddLeagueImagesAsync(IList<LeagueImageViewModel> leagueImagesViewModel, CancellationToken ct = default(CancellationToken));
    Task<List<LeagueImageViewModel>> GetAllLeagueImagesAsync(CancellationToken ct = default(CancellationToken));
    Task<LeagueImageViewModel> GetLeagueImageByIdAsync(long? id, CancellationToken ct = default(CancellationToken));
    Task<LeagueImageViewModel> AddLeagueImageAsync(LeagueImageViewModel leagueImageViewModel, CancellationToken ct = default(CancellationToken));
    Task<bool> UpdateLeagueImageAsync(LeagueImageViewModel leagueImageViewModel, CancellationToken ct = default(CancellationToken));
    Task<bool> DeleteLeagueImageAsync(long? leagueImageId, CancellationToken ct = default(CancellationToken));
    Task<List<LeagueImageViewModel>> UpdateLeagueImagesOrderAsync(List<LeagueImageViewModel> leagueImages, CancellationToken ct = default(CancellationToken));

    #endregion
  }
}