using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Gallery;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.Models.Team;
using ThePLeagueDomain.Repositories;
using ThePLeagueDomain.Repositories.Team;

namespace ThePLeagueDataCore.Repositories.Gallery
{
    public class TeamRepository : ITeamRepository
    {
        #region Fields and Properties
        private readonly ThePLeagueContext _dbContext;

        #endregion

        #region Constructor
        public TeamRepository(ThePLeagueContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #endregion

        #region Methods

        #region Team SignUp
        public async Task<Contact> AddTeamContactAsync(Contact teamContact, CancellationToken ct = default)
        {
            this._dbContext.TeamsContact.Add(teamContact);
            await this._dbContext.SaveChangesAsync(ct);

            return teamContact;
        }

        public async Task<TeamSignUpForm> AddSignUpFormAsync(TeamSignUpForm teamSignUpForm, CancellationToken ct = default)
        {
            this._dbContext.TeamSignUpForms.Add(teamSignUpForm);
            await this._dbContext.SaveChangesAsync(ct);

            return teamSignUpForm;
        }

        public Task<bool> DeleteSignUpFormAsync(long? id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<TeamSignUpForm>> GetAllSignUpFormsAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Contact> GetTeamContactByTeamId(long? teamId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<TeamSignUpForm> GetTeamSignUpFormByIdAsync(long? id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSignUpFormAsync(TeamSignUpForm leagueImage, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Team

        private async Task<bool> TeamExists(string id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public async Task<Team> GetByIdAsync(string id, CancellationToken ct = default)
        {
            return await this._dbContext.Teams.FindAsync(id);
        }
        public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        {
            if(!await TeamExists(id, ct))
            {
                return false;
            }

            Team teamToDelete = this._dbContext.Teams.Find(id);
            this._dbContext.Teams.Remove(teamToDelete);
            await this._dbContext.SaveChangesAsync(ct);
            return true;
        }

        public async Task<Team> AddAsync(Team newTeam, CancellationToken ct = default)
        {
            this._dbContext.Teams.Add(newTeam);
            await this._dbContext.SaveChangesAsync(ct);

            return newTeam;
        }

        public async Task<bool> UpdateAsync(Team updatedTeam, CancellationToken ct = default)
        {
            if(!await this.TeamExists(updatedTeam.Id))
            {
                return false;
            }

            this._dbContext.Teams.Update(updatedTeam);
            await this._dbContext.SaveChangesAsync(ct);
            return true;
        }

        #endregion

        #endregion
    }
}