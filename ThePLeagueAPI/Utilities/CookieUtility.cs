using System;
using Microsoft.AspNetCore.Http;
using ThePLeagueDomain.Models;

namespace ThePLeagueAPI.Utilities
{
  public class CookieUtility
  {
    private static readonly CookieOptions _cookieOptions = new CookieOptions()
    {
      Expires = DateTime.Now.AddDays(14),
      SameSite = SameSiteMode.None,
      HttpOnly = true,
      Secure = true
    };
    public static void GenerateHttpOnlyCookie(HttpResponse response, string cookieName, ApplicationToken token)
    {
      response.Cookies.Append(cookieName, token.access_token, _cookieOptions);
    }

    public static void RemoveCookie(HttpResponse response, string cookieName)
    {
      response.Cookies.Delete(cookieName);
    }
  }
}