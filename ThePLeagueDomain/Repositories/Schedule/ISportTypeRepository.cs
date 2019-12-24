using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDomain.Repositories.Schedule
{
    public interface ISportTypeRepository
    {
        #region Methods

        Task<SportType> GetByIdAsync(string id, CancellationToken ct = default(CancellationToken));
        Task<List<SportType>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<SportType> AddAsync(SportType newSportType, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(SportType sportTypeToUpdate, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(string id, CancellationToken ct = default(CancellationToken));

        #endregion
    }
}
