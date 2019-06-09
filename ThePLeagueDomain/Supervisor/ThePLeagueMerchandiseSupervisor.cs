using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Converters;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.ViewModels;
using ThePLeagueDomain.ViewModels.Merchandise;

namespace ThePLeagueDomain.Supervisor
{
  public partial class ThePLeagueSupervisor : IThePLeagueSupervisor
  {

    #region GearItem
    public async Task<List<GearItemViewModel>> GetAllGearItemsAsync(CancellationToken ct = default)
    {
      List<GearItemViewModel> gearItemsViewModel = GearItemConverter.ConvertList(await this._gearItemRepository.GetAllAsync());

      return gearItemsViewModel;
    }
    public async Task<GearItemViewModel> GetGearItemByIdAsync(long gearItemId, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    public async Task<GearItemViewModel> AddGearItemAsync(GearItemViewModel gearItemViewModel, CancellationToken ct = default)
    {
      foreach (GearImageViewModel gearImageViewModel in gearItemViewModel.Images)
      {
        GearImage newGearImage = new GearImage()
        {
          CloudinaryId = gearImageViewModel.CloudinaryId,
          GearItemId = gearItemViewModel.Id,
          Name = gearImageViewModel.Name,
          Size = gearImageViewModel.Size,
          Type = gearImageViewModel.Type,
          Url = gearImageViewModel.Url,
          Small = gearImageViewModel.Small,
          Medium = gearImageViewModel.Medium,
          Big = gearImageViewModel.Big
        };

        await _gearImageRepository.AddAsync(newGearImage, ct);
      }

      foreach (GearSizeViewModel gearSizeViewModel in gearItemViewModel.Sizes)
      {
        GearSize newGearSize = new GearSize()
        {
          GearItemId = gearItemViewModel.Id,
          Available = gearSizeViewModel.Available,
          Color = gearSizeViewModel.Color,
          Size = gearSizeViewModel.Size
        };

        await _gearSizeRepository.AddAsync(newGearSize, ct);
      }

      GearItem newGearItem = new GearItem()
      {
        Name = gearItemViewModel.Name,
        Price = gearItemViewModel.Price,
        InStock = gearItemViewModel.InStock
      };

      newGearItem = await _gearItemRepository.AddAsync(newGearItem, ct);
      gearItemViewModel.Id = newGearItem.Id;

      return gearItemViewModel;
    }
    public async Task<GearSizeViewModel> AddGearSizeToGearItemByIdAsync(long gearItemId, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    public async Task<bool> UpdateGearItemAsync(GearItemViewModel gearItemViewModel, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    public async Task<bool> DeleteGearItemAsync(long gearItemId, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    #endregion

    #region GearImage
    public async Task<List<GearImageViewModel>> GetAllGearImagesByGearItemIdAsync(long gearItemId, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    public async Task<GearImageViewModel> AddGearImageAsync(GearImageViewModel gearSizeViewModel, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    public async Task<bool> UpdateGearImageAsync(GearImageViewModel gearSizeViewModel, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    public async Task<bool> DeleteGearImageAsync(long gearImageId, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    #endregion

    #region GearSize
    public async Task<List<GearSizeViewModel>> GetAllGearSizesByGearItemIdAsync(long gearItemId, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    public async Task<GearSizeViewModel> GetGearSizeByIdAsync(long id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    public async Task<GearSizeViewModel> AddGearSizeAsync(GearSizeViewModel gearSizeViewModel, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    public async Task<bool> UpdateGearSizeAsync(GearSizeViewModel gearSizeViewModel, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    public async Task<bool> DeleteGearSizeAsync(long gearSizeId, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    #endregion

  }
}