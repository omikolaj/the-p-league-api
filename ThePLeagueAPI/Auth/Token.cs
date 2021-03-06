using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ThePLeagueAPI.Auth.Jwt;
using ThePLeagueAPI.Auth.Jwt.JwtFactory;
using ThePLeagueDomain.Models;

namespace ThePLeagueAPI.Auth
{
  public class Token
  {
    #region Fields and Properties
    private readonly IConfiguration _configuration;

    #endregion

    #region Constructor
    public Token(IConfiguration configuration)
    {
      this._configuration = configuration;
    }
    #endregion    

    #region Methods
    public static async Task<ApplicationToken> GenerateJwt(string userName,
                                                    ClaimsIdentity identity,
                                                    IJwtFactory jwtFactory,
                                                    JwtIssuerOptions jwtOptions,
                                                    JsonSerializerSettings serializerSettings)
    {
      ApplicationToken accessToken = new ApplicationToken
      {
        access_token = await jwtFactory.GenerateEncodedToken(userName, identity),
        expires_in = (long)jwtOptions.ValidFor.TotalSeconds
      };

      return accessToken;
    }

    public static string GenerateRefreshToken()
    {
      var randomNumber = new byte[32];
      using (var rng = RandomNumberGenerator.Create())
      {
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
      }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
      var jwtAppSettingOptions = _configuration.GetSection(nameof(JwtIssuerOptions));

      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateAudience = true,
        ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

        ValidateIssuer = true,
        ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration[nameof(VaultKeys.JwtSecret)])),
        ValidateLifetime = false
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      SecurityToken securityToken;
      var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
      var jwtSecurityToken = securityToken as JwtSecurityToken;

      if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
      {
        throw new SecurityTokenException("Invalid token");
      }
      return principal;
    }

    #endregion

  }
}
