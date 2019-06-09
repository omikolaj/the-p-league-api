using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ThePLeagueAPI.Auth.Errors
{
  public class ErrorDescriptions
  {
    public const string LoginFailure = "Invalid username or password.";
    public const string RefreshTokenFailure = "Invalid or expired refresh token";

    public const string UserNotFoundFailure = "This user was not found in the database";

  }
}