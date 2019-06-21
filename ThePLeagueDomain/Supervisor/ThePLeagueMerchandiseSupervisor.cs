using System;
using System.Collections.Generic;
using System.Linq;
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
    public async Task<GearItemViewModel> GetGearItemByIdAsync(long? gearItemId, CancellationToken ct = default)
    {
      GearItemViewModel gearitemViewModel = GearItemConverter.Convert(await this._gearItemRepository.GetByIDAsync(gearItemId, ct));
      gearitemViewModel.Images = await GetAllGearImagesByGearItemIdAsync(gearItemId, ct);
      gearitemViewModel.Sizes = await GetAllGearSizesByGearItemIdAsync(gearItemId, ct);

      return gearitemViewModel;
    }
    public async Task<GearItemViewModel> AddGearItemAsync(GearItemViewModel gearItemViewModel, CancellationToken ct = default)
    {
      List<GearImageViewModel> gearImages = new List<GearImageViewModel>();
      List<GearSizeViewModel> gearSizes = new List<GearSizeViewModel>();

      GearItem newGearItem = new GearItem()
      {
        Name = gearItemViewModel.Name,
        Price = gearItemViewModel.Price,
        InStock = gearItemViewModel.InStock
      };

      newGearItem = await _gearItemRepository.AddAsync(newGearItem, ct);
      gearItemViewModel.Id = newGearItem.Id;

      foreach (GearImageViewModel gearImageViewModel in gearItemViewModel.Images)
      {
        gearImageViewModel.GearItemId = gearItemViewModel.Id;

        gearImages.Add(await AddGearImageAsync(gearImageViewModel));
      }

      foreach (GearSizeViewModel gearSizeViewModel in gearItemViewModel.Sizes)
      {
        gearSizeViewModel.GearItemId = gearItemViewModel.Id;

        gearSizes.Add(await AddGearSizeAsync(gearSizeViewModel));
      }

      gearItemViewModel.Images = gearImages;
      gearItemViewModel.Sizes = gearSizes;

      return gearItemViewModel;
    }
    public async Task<GearSizeViewModel> AddGearSizeToGearItemByIdAsync(long gearItemId, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    public async Task<bool> UpdateGearItemAsync(GearItemViewModel gearItemViewModel, CancellationToken ct = default)
    {
      GearItem gearItem = await _gearItemRepository.GetByIDAsync(gearItemViewModel.Id, ct);

      if (gearItem == null)
      {
        return false;
      }

      gearItem.Name = gearItemViewModel.Name;
      gearItem.InStock = gearItemViewModel.InStock;
      gearItem.Price = gearItemViewModel.Price;

      // If incoming gearItemViewModel image count is smaller than existing, this means we are removing images 
      if (gearItemViewModel.Images.Count() < gearItem.Images.Count())
      {
        IList<GearImage> deleteGearImages = gearItem.Images.Where(existingGearImage => !gearItemViewModel.Images.Any(updatedGearImage => updatedGearImage.Id == existingGearImage.Id)).ToList();

        // Delete all images marked for deletion
        int length = deleteGearImages.Count();
        for (int i = 0; i < length; i++)
        {
          GearImage gearImage = deleteGearImages.ElementAt(i);
          await this._gearImageRepository.DeleteAsync(gearImage.Id, ct);
        }

        // Add all new incoming images
        foreach (GearImageViewModel gearImage in gearItemViewModel.NewImages)
        {
          gearImage.GearItemId = gearItem.Id;
          await this.AddGearImageAsync(gearImage, ct);
        }
      }

      // Update incoming sizes
      foreach (GearSizeViewModel gearSize in gearItemViewModel.Sizes)
      {
        await this.UpdateGearSizeAsync(gearSize, ct);
      }

      return await this._gearItemRepository.UpdateAsync(gearItem);

    }
    public async Task<bool> DeleteGearItemAsync(long? gearItemId, CancellationToken ct = default)
    {
      GearItemViewModel gearItemToDelete = GearItemConverter.Convert(await this._gearItemRepository.GetByIDAsync(gearItemId, ct));

      if (gearItemToDelete == null)
      {
        return false;
      }

      if (gearItemToDelete.Images.Count() > 0)
      {
        // Delete gearImages from database
        foreach (GearImageViewModel gearImage in gearItemToDelete.Images)
        {
          await this._gearImageRepository.DeleteAsync(gearImage.Id, ct);
        }
      }

      foreach (GearSizeViewModel size in gearItemToDelete.Sizes)
      {
        await this._gearSizeRepository.DeleteAsync(size.Id, ct);
      }

      return await this._gearItemRepository.DeleteAsync(gearItemId, ct);
    }
    #endregion

    #region GearImage
    public async Task<List<GearImageViewModel>> GetAllGearImagesByGearItemIdAsync(long? gearItemId, CancellationToken ct = default)
    {
      return GearImageConverter.ConvertList(await this._gearImageRepository.GetAllByGearItemIdAsync(gearItemId, ct));
    }
    public async Task<GearImageViewModel> AddGearImageAsync(GearImageViewModel gearImageViewModel, CancellationToken ct = default)
    {
      GearImage newGearImage = new GearImage()
      {
        GearItemId = gearImageViewModel.GearItemId,
        CloudinaryPublicId = gearImageViewModel.CloudinaryPublicId,
        Name = gearImageViewModel.Name,
        Size = gearImageViewModel.Size,
        Type = gearImageViewModel.Type,
        Url = gearImageViewModel.Url,
        Small = gearImageViewModel.Small,
        Medium = gearImageViewModel.Medium,
        Big = gearImageViewModel.Big,
        ResourceType = gearImageViewModel.ResourceType,
        Width = gearImageViewModel.Width,
        Height = gearImageViewModel.Height,
        Format = gearImageViewModel.Format,
      };

      newGearImage = await _gearImageRepository.AddAsync(newGearImage, ct);
      gearImageViewModel.Id = newGearImage.Id;

      return gearImageViewModel;
    }
    public async Task<bool> UpdateGearImageAsync(GearImageViewModel gearSizeViewModel, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    public async Task<bool> DeleteGearImageAsync(long? gearImageId, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    #endregion

    #region GearSize
    public async Task<List<GearSizeViewModel>> GetAllGearSizesByGearItemIdAsync(long? gearItemId, CancellationToken ct = default)
    {
      return GearSizeConverter.ConvertList(await this._gearSizeRepository.GetAllByGearItemIdAsync(gearItemId, ct));
    }
    public async Task<GearSizeViewModel> GetGearSizeByIdAsync(long id, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
    public async Task<GearSizeViewModel> AddGearSizeAsync(GearSizeViewModel gearSizeViewModel, CancellationToken ct = default)
    {
      GearSize newGearSizeForItem = new GearSize()
      {
        GearItemId = gearSizeViewModel.GearItemId,
        Available = gearSizeViewModel.Available,
        Size = gearSizeViewModel.Size,
        Color = gearSizeViewModel.Color
      };

      newGearSizeForItem = await _gearSizeRepository.AddAsync(newGearSizeForItem, ct);
      gearSizeViewModel.Id = newGearSizeForItem.Id;

      return gearSizeViewModel;
    }
    public async Task<bool> UpdateGearSizeAsync(GearSizeViewModel gearSizeViewModel, CancellationToken ct = default)
    {
      GearSize gearSize = await _gearSizeRepository.GetByIDAsync(gearSizeViewModel.Id, ct);

      if (gearSize == null)
      {
        return false;
      }

      gearSize.Available = gearSizeViewModel.Available;
      gearSize.Color = gearSizeViewModel.Color;
      gearSize.Size = gearSizeViewModel.Size;

      return await this._gearSizeRepository.UpdateAsync(gearSize, ct);
    }
    public async Task<bool> DeleteGearSizeAsync(long gearSizeId, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }

    #endregion

  }
}