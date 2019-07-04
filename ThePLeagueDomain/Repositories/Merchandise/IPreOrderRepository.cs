using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Merchandise;

namespace ThePLeagueDomain.Repositories.Merchandise
{
  public interface IPreOrderRepository
  {
    #region Methods
    Task<PreOrder> GetByIDAsync(long? id, CancellationToken ct = default(CancellationToken));
    Task<PreOrder> AddAsync(PreOrder preOrder, CancellationToken ct = default(CancellationToken));
    Task<PreOrderContact> AddPreOrderContactAsync(PreOrderContact preOrderContact, CancellationToken ct = default(CancellationToken));

    #endregion
  }
}