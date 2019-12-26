using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<LeagueViewModel> GetLeagueByIdAsync(string id, CancellationToken ct = default(CancellationToken))
        {
            LeagueViewModel league = LeagueConverter.Convert(await this._leagueRepository.GetByIdAsync(id, ct));

            return league;
        }
        public async Task<LeagueViewModel> AddLeagueAsync(LeagueViewModel newLeague, CancellationToken ct = default(CancellationToken))
        {
            League league = new League()
            {
                Selected = newLeague.Selected,
                SportTypeID = newLeague.SportTypeID,
                Type = newLeague.Type
            };

            league = await this._leagueRepository.AddAsync(league, ct);
            newLeague.Id = league.Id;

            return newLeague;
        }
        public async Task<bool> UpdateLeagueAsync(LeagueViewModel leagueToUpdate, CancellationToken ct = default(CancellationToken))
        {
            League league = await this._leagueRepository.GetByIdAsync(leagueToUpdate.Id, ct);

            if(league == null)
            {
                return false;
            }

            league.Selected = leagueToUpdate.Selected;
            league.SportTypeID = leagueToUpdate.SportTypeID;
            league.Type = leagueToUpdate.Type;
            league.Name = leagueToUpdate.Name;

            return await this._leagueRepository.UpdateAsync(league, ct);
        }
        public async Task<bool> UpdateLeaguesAsync(List<LeagueViewModel> leaguesToUpdate, CancellationToken ct = default(CancellationToken))
        {
            List<bool> updateOperations = new List<bool>();

            foreach (LeagueViewModel leagueViewModel in leaguesToUpdate)
            {
                updateOperations.Add(await UpdateLeagueAsync(leagueViewModel, ct));
            }

            // check if all succeeded
            return updateOperations.All(op => op == true);
        }
        public async Task<bool> DeleteLeagueAsync(string id, CancellationToken ct = default(CancellationToken))
        {
            LeagueViewModel leagueToDelete = LeagueConverter.Convert(await this._leagueRepository.GetByIdAsync(id, ct));

            if(leagueToDelete == null)
            {
                return false;
            }

            return await this._teamRepository.DeleteAsync(leagueToDelete.Id, ct);
        }

        #endregion
    }
}
