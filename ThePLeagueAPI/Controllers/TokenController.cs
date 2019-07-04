using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ThePLeagueAPI.Auth;
using ThePLeagueAPI.Auth.Errors;
using ThePLeagueAPI.Auth.Jwt;
using ThePLeagueAPI.Auth.Jwt.JwtFactory;
using ThePLeagueAPI.Filters;
using ThePLeagueAPI.Helpers;
using ThePLeagueAPI.Utilities;
using ThePLeagueDomain;
using ThePLeagueDomain.Models;

namespace ThePLeagueAPI.Controllers
{
  [AllowAnonymous]
  [Route("api/[controller]")]
  [Produces("application/json")]
  public class TokenController : ThePLeagueBaseController
  {
    #region Fields and Properties
    private readonly Token _token;
    private readonly IJwtFactory _jwtFactory;
    private readonly JwtIssuerOptions _jwtOptions;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly GetIdentity _getIdentity;
    private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
    {
      Formatting = Formatting.Indented
    };

    #endregion

    #region Constructor

    public TokenController(IThePLeagueSupervisor moviesPlaceSupervisor,
      Token token,
      IJwtFactory jwtFactory,
      IOptions<JwtIssuerOptions> jwtOptions,
      UserManager<ApplicationUser> userManager,
      GetIdentity getIdentity)
    {
      this._token = token;
      this._jwtFactory = jwtFactory;
      this._jwtOptions = jwtOptions.Value;
      this._userManager = userManager;
      this._getIdentity = getIdentity;
    }

    #endregion

    #region Controllers

    [HttpGet("refresh")]
    [ServiceFilter(typeof(CookieFilter))]
    public async Task<ActionResult> Refresh()
    {
      // Retrieve the application_token from cookies
      string expiredJwt = Request.Cookies.SingleOrDefault(cookie => cookie.Key == TokenOptionsStrings.ApplicationToken).Value;

      ClaimsPrincipal principal = this._token.GetPrincipalFromExpiredToken(expiredJwt);

      string userName = principal.Identity.Name;

      ApplicationUser user = _userManager.Users.SingleOrDefault(u => u.UserName == userName);

      if (user == null)
      {
        return BadRequest(Errors.AddErrorToModelState(ErrorCodes.UserNotFound, ErrorDescriptions.UserNotFoundFailure, ModelState));
      }

      string refreshToken = await _userManager.GetAuthenticationTokenAsync(user, TokenOptionsStrings.RefreshTokenProvider, TokenOptionsStrings.RefreshToken);
      // Validate Refresh Token is still valid
      bool isValid = await _userManager.VerifyUserTokenAsync(user, TokenOptionsStrings.RefreshTokenProvider, TokenOptionsStrings.RefreshToken, refreshToken);
      if (!isValid)
      {
        return BadRequest(Errors.AddErrorToModelState(ErrorCodes.RefreshToken, ErrorDescriptions.RefreshTokenFailure, ModelState));
      }

      ClaimsIdentity identity = await _getIdentity.GenerateClaimsIdentity(user);

      if (identity == null)
      {
        return BadRequest(Errors.AddErrorToModelState(ErrorCodes.RefreshToken, ErrorDescriptions.RefreshTokenFailure, ModelState));
      }

      // Remove old refresh token from the database
      await _userManager.RemoveAuthenticationTokenAsync(user, TokenOptionsStrings.RefreshTokenProvider, TokenOptionsStrings.RefreshToken);

      // Generate a new one
      string newRefreshToken = await _userManager.GenerateUserTokenAsync(user, TokenOptionsStrings.RefreshTokenProvider, TokenOptionsStrings.RefreshToken);

      // Set it in the database
      await _userManager.SetAuthenticationTokenAsync(user, TokenOptionsStrings.RefreshTokenProvider, TokenOptionsStrings.RefreshToken, newRefreshToken);

      // Generate new jwt
      ApplicationToken token = await Token.GenerateJwt(user.UserName, identity, this._jwtFactory, this._jwtOptions, this._jsonSerializerSettings);

      CookieUtility.RemoveCookie(Response, TokenOptionsStrings.ApplicationToken);

      CookieUtility.GenerateHttpOnlyCookie(Response, TokenOptionsStrings.ApplicationToken, token);

      return new OkObjectResult(token);

    }

    #endregion
  }
}