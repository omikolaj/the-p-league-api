using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Gallery;
using ThePLeagueDomain.Models.Team;

namespace ThePLeagueDomain.Repositories.TeamSignUp
{
    public interface ITeamSignUpRepository
    {
        #region Methods

        #region Team SignUp
        Task<TeamSignUpForm> GetTeamSignUpFormByIdAsync(long? id, CancellationToken ct = default(CancellationToken));
        Task<Contact> GetTeamContactByTeamId(long? teamId, CancellationToken ct = default(CancellationToken));
        Task<List<TeamSignUpForm>> GetAllSignUpFormsAsync(CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteSignUpFormAsync(long? id, CancellationToken ct = default(CancellationToken));
        Task<TeamSignUpForm> AddSignUpFormAsync(TeamSignUpForm teamSignUpForm, CancellationToken ct = default(CancellationToken));
        Task<Contact> AddTeamContactAsync(Contact teamContact, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateSignUpFormAsync(TeamSignUpForm leagueImage, CancellationToken ct = default(CancellationToken));

        #endregion

        #endregion
    }
}