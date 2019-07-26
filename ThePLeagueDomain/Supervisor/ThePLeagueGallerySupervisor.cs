using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Converters.GalleryConverters;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Gallery;
using ThePLeagueDomain.ViewModels;
using ThePLeagueDomain.ViewModels.Gallery;
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
      List<LeagueImageViewModel> allLeagueImages = leagueImagesViewModel.Concat(await this.GetAllLeagueImagesAsync()).OrderBy(o => o.OrderId).ToList();
      for (int i = 0; i < allLeagueImages.Count(); i++)
      {
        LeagueImageViewModel leagueImage = allLeagueImages.ElementAt(i);
        if (allLeagueImages.ElementAt(i).OrderId == null)
        {
          leagueImage.OrderId = i + 1;
          addedLeagueImages.Add(await this.AddLeagueImageAsync(leagueImage, ct));
          continue;
        }
        leagueImage.OrderId = i + 1;
        await UpdateLeagueImageAsync(leagueImage, ct);
      }

      return addedLeagueImages;
    }

    public async Task<LeagueImageViewModel> GetLeagueImageByIdAsync(long? id, CancellationToken ct = default(CancellationToken))
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
        Format = leagueImageViewModel.Format,
        OrderId = leagueImageViewModel.OrderId
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

    public async Task<bool> UpdateLeagueImageAsync(LeagueImageViewModel leagueImageViewModel, CancellationToken ct = default)
    {
      LeagueImage leagueImageUpdate = await this._leagueImageRepository.GetByIDAsync(leagueImageViewModel.Id, ct);

      if (leagueImageUpdate == null)
      {
        return false;
      }

      leagueImageUpdate.CloudinaryPublicId = leagueImageViewModel.CloudinaryPublicId;
      leagueImageUpdate.Name = leagueImageViewModel.Name;
      leagueImageUpdate.Size = leagueImageViewModel.Size;
      leagueImageUpdate.Type = leagueImageViewModel.Type;
      leagueImageUpdate.Url = leagueImageViewModel.Url;
      leagueImageUpdate.Small = leagueImageViewModel.Small;
      leagueImageUpdate.Medium = leagueImageViewModel.Medium;
      leagueImageUpdate.Big = leagueImageViewModel.Big;
      leagueImageUpdate.ResourceType = leagueImageViewModel.ResourceType;
      leagueImageUpdate.Width = leagueImageViewModel.Width;
      leagueImageUpdate.Height = leagueImageViewModel.Height;
      leagueImageUpdate.Format = leagueImageViewModel.Format;
      leagueImageUpdate.OrderId = leagueImageViewModel.OrderId;

      return await this._leagueImageRepository.UpdateAsync(leagueImageUpdate, ct);
    }

    public async Task<List<LeagueImageViewModel>> UpdateLeagueImagesOrderAsync(List<LeagueImageViewModel> leagueImages, CancellationToken ct = default)
    {
      for (int i = 0; i < leagueImages.Count; i++)
      {
        // if one of the saves did not work we are just going to slielntly ignore it and continue
        // taking the risk on this, maybe it wont be a headache in the future.
        if (!await this.UpdateLeagueImageAsync(leagueImages.ElementAt(i), ct))
        {
          // if one of the leagueImage updates fail return null
          return null;
        }
      }

      return leagueImages;
    }

    #endregion
  }
}