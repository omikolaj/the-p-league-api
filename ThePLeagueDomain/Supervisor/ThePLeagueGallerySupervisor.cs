using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Converters;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Gallery;
using ThePLeagueDomain.ViewModels;
using ThePLeagueDomain.ViewModels.Merchandise;

namespace ThePLeagueDomain.Supervisor
{
  public partial class ThePLeagueSupervisor : IThePLeagueSupervisor
  {
    #region Methods
    public async Task<List<LeagueImageViewModel>> GetAllLeagueImagesAsync(CancellationToken ct = default)
    {
      List<LeagueImageViewModel> leagueImagesViewModel = LeagueImageConverter.ConvertList(await this._leagueImageRepository.GetAllAsync());

      return leagueImagesViewModel;
    }

    public async Task<List<LeagueImageViewModel>> AddLeagueImagesAsync(IList<LeagueImageViewModel> leagueImagesViewModel, CancellationToken ct = default)
    {
      List<LeagueImageViewModel> addedLeagueImages = new List<LeagueImageViewModel>();
      foreach (LeagueImageViewModel leagueImage in leagueImagesViewModel)
      {
        addedLeagueImages.Add(await this.AddLeagueImageAsync(leagueImage, ct));
      }

      return addedLeagueImages;
    }

    public async Task<LeagueImageViewModel> GetLeagueImageByIdAsync(long id, CancellationToken ct = default(CancellationToken))
    {
      LeagueImageViewModel leagueImageViewModel = LeagueImageConverter.Convert(await this._leagueImageRepository.GetByIDAsync(id, ct));

      return leagueImageViewModel;
    }

    public async Task<LeagueImageViewModel> AddLeagueImageAsync(LeagueImageViewModel leagueImageViewModel, CancellationToken ct = default)
    {
      LeagueImage newLeagueImage = new LeagueImage()
      {
        CloudinaryPublicId = leagueImageViewModel.CloudinaryPublicId,
        Name = leagueImageViewModel.Name,
        Size = leagueImageViewModel.Size,
        Type = leagueImageViewModel.Type,
        Url = leagueImageViewModel.Url,
        Small = leagueImageViewModel.Small,
        Medium = leagueImageViewModel.Medium,
        Big = leagueImageViewModel.Big,
        ResourceType = leagueImageViewModel.ResourceType,
        Width = leagueImageViewModel.Width,
        Height = leagueImageViewModel.Height,
        Format = leagueImageViewModel.Format
      };

      newLeagueImage = await _leagueImageRepository.AddAsync(newLeagueImage, ct);
      leagueImageViewModel.Id = newLeagueImage.Id;

      return leagueImageViewModel;
    }

    public async Task<bool> DeleteLeagueImageAsync(long? leagueImageId, CancellationToken ct = default)
    {
      LeagueImageViewModel leagueImageToDelete = LeagueImageConverter.Convert(await this._leagueImageRepository.GetByIDAsync(leagueImageId, ct));

      if (leagueImageToDelete == null)
      {
        return false;
      }

      return await this._leagueImageRepository.DeleteAsync(leagueImageToDelete.Id, ct);
    }

    public Task<bool> UpdateLeagueImageAsync(LeagueImageViewModel leagueImageViewModel, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    #endregion
  }
}