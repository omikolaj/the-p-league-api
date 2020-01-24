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

            // if sport type successfully updated, update all of its leagues
            if(await this._sportTypeRepository.UpdateAsync(sportType, ct))
            {
                // when the sport type name changes we also need to update all of the associated leagues type property
                // since the type property on the league represents the name of the sport
                List<LeagueViewModel> leagues = await GetLeaguesBySportTypeIdAsync(sportType.Id);
                List<LeagueViewModel> updatedLeagues = new List<LeagueViewModel>();

                foreach (LeagueViewModel league in leagues)
                {
                    league.Type = sportTypeToUpdate.Name;
                    updatedLeagues.Add(league);
                }

                return await UpdateLeaguesAsync(updatedLeagues, ct);
            }

            // if updating sport type failed
            return false;
        }
        public async Task<bool> DeleteSportTypeAsync(string id, CancellationToken ct = default(CancellationToken))
        {
            SportTypeViewModel sportTypeToDelete = SportTypeConverter.Convert(await this._sportTypeRepository.GetByIdAsync(id, ct));

            if(sportTypeToDelete == null)
            {
                return false;
            }

            sportTypeToDelete.Active = false;
            return await UpdateSportTypeAsync(sportTypeToDelete, ct);
        }

        #endregion
    }
}
