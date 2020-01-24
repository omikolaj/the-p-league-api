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
        #region Constants

        public const string UNASSIGNED = "-1";

        #endregion

        #region Methods

        public async Task<TeamViewModel> GetTeamByIdAsync(string id, CancellationToken ct = default(CancellationToken))
        {
            TeamViewModel team = TeamConverter.Convert(await this._teamRepository.GetByIdAsync(id));

            return team;
        }
        public async Task<List<TeamViewModel>> GetTeamsByLeagueId(string leagueID, CancellationToken ct = default(CancellationToken))
        {
            // given league ID retrieve all teams
            List<TeamViewModel> teams = TeamConverter.ConvertList(await this._teamRepository.GetAllByLeagueIdAsync(leagueID, ct));

            return teams;            
        }

        public async Task<List<TeamViewModel>> GetTeamsByIdsAsync(List<string> ids, CancellationToken ct = default(CancellationToken))
        {
            List<TeamViewModel> teams = new List<TeamViewModel>();

            foreach (string teamId in ids)
            {
                teams.Add(await GetTeamByIdAsync(teamId, ct));
            }

            return teams;
        }
        public async Task<TeamViewModel> AddTeamAsync(TeamViewModel newTeam, CancellationToken ct = default(CancellationToken))
        {
            Team team = new Team()
            {
                LeagueID = newTeam.LeagueID,
                Name = newTeam.Name,
                Selected = newTeam.Selected
            };

            team = await _teamRepository.AddAsync(team, ct);
            newTeam.Id = team.TeamId;

            return newTeam;
        }
        public async Task<bool> UpdateTeamAsync(TeamViewModel teamToUpdateViewModel, CancellationToken ct = default(CancellationToken))
        {
            Team teamToUpdate = await this._teamRepository.GetByIdAsync(teamToUpdateViewModel.Id, ct);

            if(teamToUpdate == null)
            {
                return false;
            }

            teamToUpdate.LeagueID = teamToUpdateViewModel.LeagueID ?? teamToUpdate.LeagueID;
            teamToUpdate.Name = teamToUpdateViewModel.Name ?? teamToUpdate.Name;
            teamToUpdate.Selected = teamToUpdateViewModel.Selected;
            teamToUpdate.Active = teamToUpdateViewModel.Active;

            return await this._teamRepository.UpdateAsync(teamToUpdate, ct);

        }
        public async Task<bool> UpdateTeamsAsync(List<TeamViewModel> teamsToUpdate, CancellationToken ct = default(CancellationToken))
        {
            List<bool> updateOperations = new List<bool>();

            foreach (TeamViewModel team in teamsToUpdate)
            {
                updateOperations.Add(await UpdateTeamAsync(team, ct));
            }

            // check if all succeeded
            return updateOperations.All(op => op == true);
        }
        public async Task<List<TeamViewModel>> AssignTeamsAsync(List<TeamViewModel> teamsToAssign, CancellationToken ct = default(CancellationToken))
        {
            for (int i = 0; i < teamsToAssign.Count; i++)
            {
                TeamViewModel teamViewModel = teamsToAssign.ElementAt(i);
                Team team = await this._teamRepository.GetByIdAsync(teamViewModel.Id);
                if(team != null)
                {
                    team.LeagueID = teamViewModel.LeagueID;
                    team.Selected = false;
                    await this._teamRepository.UpdateAsync(team, ct);
                }
            }

            return teamsToAssign;
        }
        public async Task<List<string>> UnassignTeamsAsync(List<string> teamsIdsToUnassignFromLeague, CancellationToken ct = default(CancellationToken))
        {
            for (int i = 0; i < teamsIdsToUnassignFromLeague.Count; i++)
            {
                string unassignId = teamsIdsToUnassignFromLeague.ElementAt(i);
                Team team = await this._teamRepository.GetByIdAsync(unassignId, ct);
                if (team != null)
                {
                    // unassign team from the league
                    team.LeagueID = null;
                    team.Selected = false;
                    await this._teamRepository.UpdateAsync(team, ct);
                }
            }

            return teamsIdsToUnassignFromLeague;
        }

        public async Task<List<TeamViewModel>> GetUnassignedTeams(CancellationToken ct = default(CancellationToken))
        {
            List<TeamViewModel> unassignedTeams = TeamConverter.ConvertList(await this._teamRepository.GetUnassignedTeams(ct));

            foreach (TeamViewModel team in unassignedTeams)
            {
                team.LeagueID = UNASSIGNED;
            }

            return unassignedTeams;
        }

        public async Task<bool> DeleteTeamAsync(string id, CancellationToken ct = default(CancellationToken))
        {
            TeamViewModel teamToDelete = TeamConverter.Convert(await this._teamRepository.GetByIdAsync(id, ct));

            if(teamToDelete == null)
            {
                return false;
            }

            return await this._teamRepository.DeleteAsync(teamToDelete.Id, ct);
        }

        public async Task<bool> DeleteTeamsAsync(List<string> ids, CancellationToken ct = default(CancellationToken))
        {
            List<bool> deleteOperations = new List<bool>();

            foreach (string deleteID in ids)
            {
                deleteOperations.Add(await DeleteTeamAsync(deleteID, ct));
            }

            return deleteOperations.All(op => op == true);
        }


        #endregion
    }
}
