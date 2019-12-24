using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.Models.TeamSignUp;
using ThePLeagueDomain.ViewModels;
using ThePLeagueDomain.ViewModels.Gallery;
using ThePLeagueDomain.ViewModels.Merchandise;
using ThePLeagueDomain.ViewModels.Team;

namespace ThePLeagueDomain
{
    public interface IThePLeagueSupervisor
    {
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

        #region PreOrder
        Task<PreOrderViewModel> AddPreOrderAsync(PreOrderViewModel preOrder, CancellationToken ct = default(CancellationToken));

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

        #region Team Sign Up
        Task<TeamSignUpFormViewModel> AddTeamSignUpFormAsync(TeamSignUpFormViewModel newTeam, CancellationToken ct = default(CancellationToken));

        #endregion

        #region Team

        Task<Team> GetTeamByIdAsync(string id, CancellationToken ct = default(CancellationToken));
        Task<Team> AddTeamAsync(Team newTeam, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateTeamAsync(Team teamToUpdate, CancellationToken ct = default(CancellationToken));
        Task<List<Team>> AssignTeamsAsync(List<Team> teamsToAssign, CancellationToken ct = default(CancellationToken));
        Task<List<Team>> UnassignTeamsAsync(List<Team> teamsToUnassign, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteTeamAsync(string id, CancellationToken ct = default(CancellationToken));

        #endregion

        #region League

        Task<League> GetLeagueByIdAsync(string id, CancellationToken ct = default(CancellationToken));
        Task<League> AddLeagueAsync(League newLeague, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateLeagueAsync(League leagueToUpdate, CancellationToken ct = default(CancellationToken));        
        Task<bool> DeleteLeagueAsync(string id, CancellationToken ct = default(CancellationToken));

        #endregion

        #region Sport Type

        Task<SportType> GetSportTypeByIdAsync(string id, CancellationToken ct = default(CancellationToken));
        Task<List<SportType>> GetAllSportTypesAsync(CancellationToken ct = default(CancellationToken));
        Task<SportType> AddSportTypeAsync(SportType newSportType, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateSportTypeAsync(SportType sportTypeToUpdate, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteSportTypeAsync(string id, CancellationToken ct = default(CancellationToken));

        #endregion

        #region League Session Schedule

        Task<LeagueSessionSchedule> PublishSessionSchedule(LeagueSessionSchedule newLeagueSessionSchedule, CancellationToken ct = default(CancellationToken));

        #endregion
    }
}