using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ThePLeagueAPI.Auth.Jwt.JwtFactory
{
  public class JwtFactory : IJwtFactory
  {
    private readonly JwtIssuerOptions _jwtOptions;

    public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
    {
      _jwtOptions = jwtOptions.Value;
      ThrowIfInvalidOptions(_jwtOptions);
    }

    public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
    {
      List<Claim> permissions = new List<Claim>();
      permissions.AddRange(identity.Claims.Select(claim => claim).Where(c => c.Type == Constants.Strings.JwtClaimIdentifiers.Permission));

      List<Claim> roles = new List<Claim>();
      roles.AddRange(identity.Claims.Select(claim => claim).Where(c => c.Type == Constants.Strings.JwtClaimIdentifiers.Role));

      var claims = new List<Claim>
      {
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(ClaimTypes.Name, userName),
                 identity.FindFirst(Constants.Strings.JwtClaimIdentifiers.Id),
             };

      claims.AddRange(permissions);
      claims.AddRange(roles);

      // Create the JWT security token and encode it.
      var jwt = new JwtSecurityToken(
          issuer: _jwtOptions.Issuer,
          audience: _jwtOptions.Audience, // Audience cannot be null
          claims: claims,
          notBefore: _jwtOptions.NotBefore,
          expires: _jwtOptions.Expiration,
          signingCredentials: _jwtOptions.SigningCredentials);

      var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

      return encodedJwt;
    }

    private static long ToUnixEpochDate(DateTime date)
      => (long)Math.Round((date.ToUniversalTime() -
                           new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                          .TotalSeconds);

    private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
    {
      if (options == null) throw new ArgumentNullException(nameof(options));

      if (options.ValidFor <= TimeSpan.Zero)
      {
        throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
      }

      if (options.SigningCredentials == null)
      {
        throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
      }

      if (options.JtiGenerator == null)
      {
        throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
      }
    }
  }
}