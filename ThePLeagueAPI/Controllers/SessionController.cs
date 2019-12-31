using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
  [AllowAnonymous]
  [Route("api/")]
  [Produces("application/json")]
  [ServiceFilter(typeof(ValidateModelStateAttribute))]
  public class SessionController : ThePLeagueBaseController
  {
    #region Fields and Properties

    private readonly IThePLeagueSupervisor _supervisor;
    private readonly IJwtFactory _jwtFactory;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly GetIdentity _getIdentity;
    private readonly JwtIssuerOptions _jwtOptions;
    private readonly Token _token;

    private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
    {
      Formatting = Formatting.Indented
    };

    #endregion

    #region Constructor
    public SessionController(
      IThePLeagueSupervisor supervisor,
      IJwtFactory jwtFactory,
      IOptions<JwtIssuerOptions> jwtOptions,
      UserManager<ApplicationUser> userManager,
      GetIdentity getIdentity,
      Token token)
    {
      this._supervisor = supervisor;
      this._jwtFactory = jwtFactory;
      this._userManager = userManager;
      this._jwtOptions = jwtOptions.Value;
      this._getIdentity = getIdentity;
      this._token = token;
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

      ApplicationToken token = await Token.GenerateJwt(user.UserName, identity, this._jwtFactory, this._jwtOptions, this._jsonSerializerSettings);

      Utilities.CookieUtility.GenerateHttpOnlyCookie(Response, TokenOptionsStrings.ApplicationToken, token);

      return new OkObjectResult(token);
    }

    [HttpPost("admin/{:id}/update-password")]
    public async Task<ActionResult<bool>> UpdatePassword([FromBody] LoginViewModel login, CancellationToken ct = default(CancellationToken))
    {
      ApplicationUser user = _userManager.Users.SingleOrDefault(u => u.UserName == login.UserName);            

      ClaimsIdentity identity = await _getIdentity.GetClaimsIdentity(user, login.Password);

      if (identity == null)
      {
        return Unauthorized(Errors.AddErrorToModelState(ErrorCodes.Login, ErrorDescriptions.LoginFailure, ModelState));
      }

      string token = await this._userManager.GeneratePasswordResetTokenAsync(user);

      IdentityResult result = await this._userManager.ResetPasswordAsync(user, token, login.NewPassword);

      if (!result.Succeeded)
      {
        return BadRequest(Errors.AddErrorToModelState(ErrorCodes.PasswordUpdate, ErrorDescriptions.PasswordUpdateFailure, ModelState));
      }

      return new OkObjectResult(true);
    }

    [HttpDelete("logout")]
    public async Task<ActionResult<bool>> Logout(long userId, CancellationToken ct = default(CancellationToken))
    {
      // Retrieve the application_token from cookies to retrieve user's username
      string jwt = Request.Cookies.SingleOrDefault(cookie => cookie.Key == TokenOptionsStrings.ApplicationToken).Value;

      if (jwt != null)
      {
        // We do not care if the token is valid, we only care that the token is 
        ClaimsPrincipal principal = this._token.GetPrincipalFromExpiredToken(jwt);

        string userName = principal.Identity.Name;

        ApplicationUser user = _userManager.Users.SingleOrDefault(u => u.UserName == userName);
        if (user == null)
        {
          return BadRequest(Errors.AddErrorToModelState(ErrorCodes.Logout, ErrorDescriptions.UserNotFoundFailure, ModelState));
        }

        // Remove the refresh token from the database
        IdentityResult result = await _userManager.RemoveAuthenticationTokenAsync(user, TokenOptionsStrings.RefreshTokenProvider, TokenOptionsStrings.RefreshToken);
        if (!result.Succeeded)
        {
          return BadRequest(Errors.AddErrorToModelState(ErrorCodes.Logout, ErrorDescriptions.RefreshTokenDeleteFailure, ModelState));
        }

        // Delete cookie with the token
        Response.Cookies.Delete(TokenOptionsStrings.ApplicationToken);
      }

      return new OkObjectResult(true);
    }

    #endregion

  }
}