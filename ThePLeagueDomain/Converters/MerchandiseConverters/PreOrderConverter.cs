using System.Collections.Generic;
using System.Linq;
using ThePLeagueDomain.Converters.TeamConverters;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.ViewModels.Merchandise;

namespace ThePLeagueDomain.Converters.MerchandiseConverters
{
  public static class PreOrderConverter
  {
    #region Methods
    public static PreOrderViewModel Convert(PreOrder preOrder)
    {
      PreOrderViewModel preOrderViewModel = new PreOrderViewModel();
      preOrderViewModel.Id = preOrder.Id;
      preOrderViewModel.GearItemId = preOrder.GearItemId;
      preOrderViewModel.Quantity = preOrder.Quantity;
      preOrderViewModel.Size = preOrder.Size;
      preOrderViewModel.Contact = PreOrderContactConverter.Convert(preOrder.Contact);

      return preOrderViewModel;
    }

    public static List<PreOrderViewModel> ConvertList(IEnumerable<PreOrder> preOrders)
    {
      return preOrders.Select(preOrder =>
      {
        PreOrderViewModel preOrderViewModel = new PreOrderViewModel();
        preOrderViewModel.Id = preOrder.Id;
        preOrderViewModel.GearItemId = preOrder.GearItemId;
        preOrderViewModel.Quantity = preOrder.Quantity;
        preOrderViewModel.Size = preOrder.Size;
        preOrderViewModel.Contact = PreOrderContactConverter.Convert(preOrder.Contact);

        return preOrderViewModel;
      }).ToList();
    }

    #endregion
  }
}