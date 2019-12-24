using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDomain.Supervisor
{
    public partial class ThePLeagueSportTypeSupervisor : IThePLeagueSupervisor
    {
        public async Task<SportType> GetSportTypeByIdAsync(string id, CancellationToken ct = default(CancellationToken))
        {

        }
        public async Task<List<SportType>> GetAllSportTypesAsync(CancellationToken ct = default(CancellationToken))
        {

        }
        public async Task<SportType> AddSportTypeAsync(SportType newSportType, CancellationToken ct = default(CancellationToken))
        {

        }
        public async Task<bool> UpdateSportTypeAsync(SportType sportTypeToUpdate, CancellationToken ct = default(CancellationToken))
        {

        }
        public async Task<bool> DeleteSportTypeAsync(string id, CancellationToken ct = default(CancellationToken))
        {

        }
    }
}
