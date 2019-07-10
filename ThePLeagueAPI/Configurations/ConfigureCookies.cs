using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ThePLeagueAPI.Configurations
{
  public static class ConfigureCookies
  {
    public static IServiceCollection ConfigureApplicationCookies(this IServiceCollection services)
    {
      services.ConfigureApplicationCookie(options =>
      {
        options.Events.OnRedirectToAccessDenied = ReplaceRedirector(HttpStatusCode.Forbidden, options.Events.OnRedirectToAccessDenied);
        options.Events.OnRedirectToLogin = ReplaceRedirector(HttpStatusCode.Unauthorized, options.Events.OnRedirectToLogin);

        options.SlidingExpiration = true;
      });

      return services;
    }

    /// Implementation for sending back an empty HTTP response back to the client without causing redirects if request is unauthorized or denied
    private static Func<RedirectContext<CookieAuthenticationOptions>, Task> ReplaceRedirector(HttpStatusCode statusCode, Func<RedirectContext<CookieAuthenticationOptions>, Task> existingRedirector) =>
    context =>
    {
      if (context.Request.Path.StartsWithSegments("/api"))
      {
        context.Response.StatusCode = (int)statusCode;
        return Task.CompletedTask;
      }
      return existingRedirector(context);
    };
  }
}