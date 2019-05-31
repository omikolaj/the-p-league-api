using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ThePLeagueAPI.Auth;
using ThePLeagueAPI.Auth.Jwt;
using ThePLeagueAPI.Auth.Jwt.JwtFactory;
using ThePLeagueAPI.Filters;
using ThePLeagueDomain;
using ThePLeagueDomain.Converters;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels;

namespace ThePLeagueAPI.Controllers
{
  [Route("api/[controller]")]
  [Produces("application/json")]
  [ServiceFilter(typeof(ValidateModelStateAttribute))]
  public class AdminController : ThePLeagueBaseController
  {
    #region Private Fields

    private readonly IThePLeagueSupervisor _supervisor;
    private readonly ILogger _logger;
    private readonly IJwtFactory _jwtFactory;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signinManager;
    private readonly JwtIssuerOptions _jwtOptions;
    private readonly CookieOptions _cookieOptions = new CookieOptions()
    {
      HttpOnly = true,
      SameSite = SameSiteMode.Strict,
      Expires = DateTime.Now.AddDays(5)
    };

    private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
    {
      Formatting = Formatting.Indented
    };

    #endregion

    #region Constructor
    public AdminController(IThePLeagueSupervisor supervisor, ILogger logger, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager)
    {
      this._supervisor = supervisor;
      this._logger = logger;
      this._jwtFactory = jwtFactory;
      this._userManager = userManager;
      this._signinManager = signinManager;
      this._jwtOptions = jwtOptions.Value;
    }

    #endregion

    #region Controllers

    [HttpPost("login")]
    [ServiceFilter(typeof(ClaimsIdentityFilter))]
    public async Task<ActionResult> AdminLogin([FromBody] LoginViewModel login, CancellationToken ct = default(CancellationToken))
    {
      ApplicationUserViewModel userViewModel = ApplicationUserConverter.Convert(_userManager.Users.SingleOrDefault(u => u.UserName == login.UserName));

      string refreshToken = Token.GenerateRefreshToken();

      await _supervisor.SaveRefreshTokenAsync(userViewModel, refreshToken);

      string jwt = await Token.GenerateJwt(login.UserName, login.Claims, this._jwtFactory, this._jwtOptions, refreshToken, this._jsonSerializerSettings);

      Utilities.CookieUtility.GenerateHttpOnlyCookie(Response, "application_token", jwt, this._cookieOptions);

      return new OkObjectResult(jwt);
    }

    #endregion

  }
}