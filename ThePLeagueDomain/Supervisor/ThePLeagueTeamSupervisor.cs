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
        private const string UNASSIGNED = "-1";

        #endregion

        #region Methods

        public async Task<TeamViewModel> GetTeamByIdAsync(string id, CancellationToken ct = default(CancellationToken))
        {
            TeamViewModel team = TeamConverter.Convert(await this._teamRepository.GetByIdAsync(id));

            return team;
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
            newTeam.Id = team.Id;

            return newTeam;
        }
        public async Task<bool> UpdateTeamAsync(TeamViewModel teamToUpdateViewModel, CancellationToken ct = default(CancellationToken))
        {
            Team teamToUpdate = await this._teamRepository.GetByIdAsync(teamToUpdateViewModel.Id, ct);

            if(teamToUpdate == null)
            {
                return false;
            }

            teamToUpdate.LeagueID = teamToUpdateViewModel.LeagueID;
            teamToUpdate.Name = teamToUpdateViewModel.Name;
            teamToUpdate.Selected = teamToUpdateViewModel.Selected;

            return await this._teamRepository.UpdateAsync(teamToUpdate, ct);

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
        public async Task<List<TeamViewModel>> UnassignTeamsAsync(List<TeamViewModel> teamsToUnassign, CancellationToken ct = default(CancellationToken))
        {
            for (int i = 0; i < teamsToUnassign.Count; i++)
            {
                TeamViewModel teamViewModel = teamsToUnassign.ElementAt(i);
                Team team = await this._teamRepository.GetByIdAsync(teamViewModel.Id);
                if (team != null)
                {
                    team.LeagueID = UNASSIGNED;
                    team.Selected = false;
                    await this._teamRepository.UpdateAsync(team, ct);
                }
            }

            return teamsToUnassign;
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

        #endregion
    }
}
