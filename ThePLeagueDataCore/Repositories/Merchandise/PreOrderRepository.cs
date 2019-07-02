using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Gallery;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.Repositories.Gallery;
using ThePLeagueDomain.Repositories.Merchandise;

namespace ThePLeagueDataCore.Repositories.Gallery
{
  public class PreOrderRepository : IPreOrderRepository
  {
    #region Fields and Properties
    private readonly ThePLeagueContext _dbContext;

    #endregion

    #region Constructor
    public PreOrderRepository(ThePLeagueContext dbContext)
    {
      this._dbContext = dbContext;
    }
    #endregion

    #region Methods
    private async Task<bool> PreOrderExists(long? id, CancellationToken ct = default(CancellationToken))
    {
      return await GetByIDAsync(id, ct) != null;
    }

    public async Task<PreOrder> GetByIDAsync(long? id, CancellationToken ct)
    {
      return await this._dbContext.PreOrders.Include(preOrder => preOrder.Contact).SingleOrDefaultAsync(preOrder => preOrder.Id == id);
    }

    public async Task<PreOrder> AddAsync(PreOrder preOrder, CancellationToken ct = default)
    {
      this._dbContext.PreOrders.Add(preOrder);
      await this._dbContext.SaveChangesAsync(ct);

      return preOrder;
    }

    public async Task<PreOrderContact> AddPreOrderContactAsync(PreOrderContact preOrderContact, CancellationToken ct = default)
    {
      this._dbContext.PreOrderContacts.Add(preOrderContact);
      await this._dbContext.SaveChangesAsync(ct);

      return preOrderContact;
    }

    #endregion
  }
}