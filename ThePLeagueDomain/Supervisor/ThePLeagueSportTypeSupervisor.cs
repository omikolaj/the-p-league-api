using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Converters.Schedule;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueDomain.Supervisor
{
    public partial class ThePLeagueSupervisor : IThePLeagueSupervisor
    {
        #region Methods

        public async Task<SportTypeViewModel> GetSportTypeByIdAsync(string id, CancellationToken ct = default(CancellationToken))
        {
            SportTypeViewModel sportType = SportTypeConverter.Convert(await this._sportTypeRepository.GetByIdAsync(id, ct));

            return sportType;
        }
        public async Task<List<SportTypeViewModel>> GetAllSportTypesAsync(CancellationToken ct = default(CancellationToken))
        {
            List<SportTypeViewModel> sportTypes = SportTypeConverter.ConvertList(await this._sportTypeRepository.GetAllAsync(ct));

            return sportTypes;
        }
        public async Task<SportTypeViewModel> AddSportTypeAsync(SportTypeViewModel newSportType, CancellationToken ct = default(CancellationToken))
        {
            SportType sportType = new SportType();
            sportType.Name = newSportType.Name;

            sportType = await _sportTypeRepository.AddAsync(sportType, ct);
            newSportType.Id = sportType.Id;

            return newSportType;
        }
        public async Task<bool> UpdateSportTypeAsync(SportTypeViewModel sportTypeToUpdate, CancellationToken ct = default(CancellationToken))
        {
            SportType sportType = await _sportTypeRepository.GetByIdAsync(sportTypeToUpdate.Id, ct);

            if(sportType == null)
            {
                return false;
            }

            sportType.Name = sportTypeToUpdate.Name;

            return await this._sportTypeRepository.UpdateAsync(sportType, ct);
        }
        public async Task<bool> DeleteSportTypeAsync(string id, CancellationToken ct = default(CancellationToken))
        {
            SportTypeViewModel sportTypeToDelete = SportTypeConverter.Convert(await this._sportTypeRepository.GetByIdAsync(id, ct));

            if(sportTypeToDelete == null)
            {
                return false;
            }

            return await this._sportTypeRepository.DeleteAsync(sportTypeToDelete.Id, ct);
        }

        #endregion
    }
}
