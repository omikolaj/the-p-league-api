using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ThePLeagueAPI.Auth.Jwt;
using ThePLeagueAPI.Auth.Jwt.JwtFactory;

namespace ThePLeagueAPI.Configurations
{
  public static class JSONWebTokenConfiguration
  {
    public static IServiceCollection ConfigureJsonWebToken(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddSingleton<IJwtFactory, JwtFactory>();
      var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

      string privateKey = jwtAppSettingOptions[nameof(JwtIssuerOptions.SigningKey)];
      SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(privateKey));

      services.Configure<JwtIssuerOptions>(options =>
      {
        options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
        options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
        options.SigningCredentials = new SigningCredentials(signingKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
      });

      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

        ValidateAudience = true,
        ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = signingKey,

        RequireSignedTokens = true,
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
      };

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(configureOptions =>
      {
        configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
        configureOptions.TokenValidationParameters = tokenValidationParameters;
        configureOptions.SaveToken = true;
        // In case of having an expired token
        configureOptions.Events = new JwtBearerEvents
        {
          OnAuthenticationFailed = context =>
          {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
              context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
          }
        };
      });

      // api user claim policy
      services.AddAuthorization(options =>
      {
        options.AddPolicy("AdminPolicy", policy =>
        {
          policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Role, "Admin");
          // Runs only against the identity created by the "Bearer" handler
          policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        });
      });

      return services;
    }
  }
}