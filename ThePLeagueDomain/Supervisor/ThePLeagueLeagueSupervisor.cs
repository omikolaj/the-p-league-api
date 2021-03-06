﻿using System;
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

        public async Task<List<LeagueViewModel>> GetLeaguesByIdsAsync(List<string> ids, CancellationToken ct = default(CancellationToken))
        {
            List<LeagueViewModel> leagues = new List<LeagueViewModel>();

            foreach (string leagueId in ids)
            {
                leagues.Add(await GetLeagueByIdAsync(leagueId, ct));
            }

            return leagues;
        }

        public async Task<List<LeagueViewModel>> GetLeaguesBySportTypeIdAsync(string sportTypeId, CancellationToken ct = default(CancellationToken))
        {
            List<LeagueViewModel> leagues = LeagueConverter.ConvertList(await this._leagueRepository.GetBySportTypeIdAsync(sportTypeId, ct));

            return leagues;
        }
        public async Task<LeagueViewModel> AddLeagueAsync(LeagueViewModel newLeague, CancellationToken ct = default(CancellationToken))
        {
            League league = new League()
            {
                Name = newLeague.Name,
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

            // only update those properties which have been set on the incoming league
            league.Selected = league.Selected;
            league.SportTypeID = leagueToUpdate.SportTypeID ?? league.SportTypeID;
            league.Type = leagueToUpdate.Type ?? league.Type;
            league.Name = leagueToUpdate.Name ?? league.Name;
            league.Active = leagueToUpdate.Active;

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

        public async Task<bool> DeleteLeaguesAsync(List<string> leagueIDsToDelete, CancellationToken ct = default(CancellationToken))
        {
            List<bool> dbOperation = new List<bool>();

            // we do not actually want to delete the league rather just mark it as inactive
            foreach (string deleteID in leagueIDsToDelete)
            {
                // unassign teams. Perhaps instead of unassigning, simply leave them?
                List<TeamViewModel> teamsToUnassign = await GetTeamsByLeagueId(deleteID, ct);
                await UnassignTeamsAsync(teamsToUnassign.Select(t => t.Id).ToList(), ct);

                List<LeagueSessionScheduleViewModel> sessionsToDeactive = await GetAllSessionsByLeagueIdAsync(deleteID, ct);

                // deactive this session
                foreach (LeagueSessionScheduleViewModel session in sessionsToDeactive)
                {   
                    // only update sessions that active 
                    if(session.Active == true)
                    {
                        session.Active = false;
                        dbOperation.Add(await UpdateSessionScheduleAsync(session, ct));
                    }                    
                }
                // marking individual matches and match results as inactive for deleted leagues
                // may be unecessary, since we need to be filtering out to display only those sessions
                // and leagues that are active

                LeagueViewModel leagueToDelete = await GetLeagueByIdAsync(deleteID, ct);

                leagueToDelete.Active = false;
                dbOperation.Add(await UpdateLeagueAsync(leagueToDelete, ct));

            }

            return dbOperation.All(op => op == true);            
        }

        #endregion
    }
}
