using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using ThePLeagueAPI.Auth;
using ThePLeagueDomain.Converters;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels;

namespace ThePLeagueAPI.Filters
{
  public class ClaimsIdentityFilter : IActionFilter
  {
    private readonly GetIdentity _getIdentity;
    public ClaimsIdentityFilter(GetIdentity getIdentity)
    {
      this._getIdentity = getIdentity;
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {

    }

    public async void OnActionExecuting(ActionExecutingContext context)
    {
      LoginViewModel loginViewModel = context.ActionArguments["login"] as LoginViewModel;

      ClaimsIdentity identity = new ClaimsIdentity();
      //ClaimsIdentity identity = await _getIdentity.GetClaimsIdentity(loginViewModel.UserName, loginViewModel.Password);

      if (identity == null)
      {
        throw new NotImplementedException();
        //return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
      }

      loginViewModel.Claims = identity;

    }
  }
}