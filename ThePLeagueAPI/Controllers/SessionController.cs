using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ThePLeagueAPI.Auth;
using ThePLeagueAPI.Auth.Errors;
using ThePLeagueAPI.Auth.Jwt;
using ThePLeagueAPI.Auth.Jwt.JwtFactory;
using ThePLeagueAPI.Filters;
using ThePLeagueAPI.Helpers;
using ThePLeagueDomain;
using ThePLeagueDomain.Converters;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels;

namespace ThePLeagueAPI.Controllers
{
  [Route("api/")]
  [Produces("application/json")]
  [AllowAnonymous]
  // [ServiceFilter(typeof(ValidateModelStateAttribute))]
  public class SessionController : ThePLeagueBaseController
  {
    #region Private Fields

    private readonly IThePLeagueSupervisor _supervisor;
    private readonly ILogger _logger;
    private readonly IJwtFactory _jwtFactory;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly GetIdentity _getIdentity;
    private readonly JwtIssuerOptions _jwtOptions;

    private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
    {
      Formatting = Formatting.Indented
    };

    #endregion

    #region Constructor
    public SessionController(IThePLeagueSupervisor supervisor, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, UserManager<ApplicationUser> userManager, GetIdentity getIdentity)
    {
      this._supervisor = supervisor;
      // this._logger = logger;
      this._jwtFactory = jwtFactory;
      this._userManager = userManager;
      this._jwtOptions = jwtOptions.Value;
      this._getIdentity = getIdentity;
    }

    #endregion

    #region Controllers

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginViewModel login, CancellationToken ct = default(CancellationToken))
    {
      ApplicationUser user = _userManager.Users.SingleOrDefault(u => u.UserName == login.UserName);

      ClaimsIdentity identity = await _getIdentity.GetClaimsIdentity(user, login.Password);

      if (identity == null)
      {
        return Unauthorized(Errors.AddErrorToModelState(ErrorCodes.Login, ErrorDescriptions.LoginFailure, ModelState));
      }

      // Remove existing refresh tokens
      await _userManager.RemoveAuthenticationTokenAsync(user, TokenOptionsStrings.RefreshTokenProvider, TokenOptionsStrings.RefreshToken);

      // Generate a new Token
      string newRefreshToken = await _userManager.GenerateUserTokenAsync(user, TokenOptionsStrings.RefreshTokenProvider, TokenOptionsStrings.RefreshToken);

      // Issue new refresh token to the user
      await _userManager.SetAuthenticationTokenAsync(user, TokenOptionsStrings.RefreshTokenProvider, TokenOptionsStrings.RefreshToken, newRefreshToken);

      // var refreshToken = await _userManager.GetAuthenticationTokenAsync(user, "ThePLeague", "RefreshToken");
      // var isValid = await _userManager.VerifyUserTokenAsync(user, "ThePLeague", "RefreshToken", refreshToken);

      //string refreshToken1 = Token.GenerateRefreshToken();

      //await _supervisor.SaveRefreshTokenAsync(userViewModel, refreshToken);

      ApplicationToken token = await Token.GenerateJwt(user.UserName, identity, this._jwtFactory, this._jwtOptions, this._jsonSerializerSettings);

      Utilities.CookieUtility.GenerateHttpOnlyCookie(Response, TokenOptionsStrings.ApplicationToken, token);

      return new OkObjectResult(token);
    }

    #endregion

  }
}