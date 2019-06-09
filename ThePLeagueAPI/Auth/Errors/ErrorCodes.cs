using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ThePLeagueAPI.Auth.Errors
{
  public class ErrorCodes
  {
    public const string Login = "login";
    public const string RefreshToken = "refresh_token";
    public const string UserNotFound = "user_not_found";
  }
}