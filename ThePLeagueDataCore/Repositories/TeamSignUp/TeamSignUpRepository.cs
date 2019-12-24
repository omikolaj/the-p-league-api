using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Gallery;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.Models.TeamSignUp;
using ThePLeagueDomain.Repositories;
using ThePLeagueDomain.Repositories.TeamSignUp;

namespace ThePLeagueDataCore.Repositories.TeamSignUp
{
    public class TeamSignUpRepository : ITeamSignUpRepository
    {
        #region Fields and Properties
        private readonly ThePLeagueContext _dbContext;

        #endregion

        #region Constructor
        public TeamSignUpRepository(ThePLeagueContext dbContext)
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

        #endregion
    }
}