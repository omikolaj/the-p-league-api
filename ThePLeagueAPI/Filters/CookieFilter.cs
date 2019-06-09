using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ThePLeagueAPI.Helpers;

namespace ThePLeagueAPI.Filters
{
  public class CookieFilter : IActionFilter
  {
    public void OnActionExecuted(ActionExecutedContext context)
    {

    }
    // Checks if the incoming request has more 0 cookies, and if the cookie list does not contain the application_token key
    // if it both are true it returns unauthorized
    public void OnActionExecuting(ActionExecutingContext context)
    {
      var cookieList = context.HttpContext.Request.Cookies;
      if ((cookieList.Count() == 0) && (!cookieList.ContainsKey(TokenOptionsStrings.ApplicationToken)))
      {
        context.Result = new UnauthorizedObjectResult(context.ModelState);
      }
    }
  }
}