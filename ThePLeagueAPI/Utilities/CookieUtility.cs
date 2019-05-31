using Microsoft.AspNetCore.Http;

namespace ThePLeagueAPI.Utilities
{
  public class CookieUtility
  {
    public static void GenerateHttpOnlyCookie(HttpResponse response, string cookieName, string jwt, CookieOptions options)
    {
      response.Cookies.Append(cookieName, jwt, options);
    }
  }
}