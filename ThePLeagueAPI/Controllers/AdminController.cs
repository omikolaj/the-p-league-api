using System;
using System.Linq;
using System.Security.Claims;
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
using ThePLeagueAPI.Auth.Errors;
using ThePLeagueAPI.Auth.Jwt;
using ThePLeagueAPI.Auth.Jwt.JwtFactory;
using ThePLeagueAPI.Filters;
using ThePLeagueAPI.Utilities;
using ThePLeagueDomain;
using ThePLeagueDomain.Converters;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels;

namespace ThePLeagueAPI.Controllers
{
  [Route("api/[controller]")]
  [Produces("application/json")]
  // [ServiceFilter(typeof(ValidateModelStateAttribute))]
  public class AdminController : ThePLeagueBaseController
  {
    #region Private Fields

    private readonly IThePLeagueSupervisor _supervisor;
    private readonly ILogger _logger;
    private readonly IJwtFactory _jwtFactory;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly GetIdentity _getIdentity;
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
    public AdminController(IThePLeagueSupervisor supervisor, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, UserManager<ApplicationUser> userManager, GetIdentity getIdentity)
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

    #endregion

  }
}