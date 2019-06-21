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
    public const string GearItemUpdateFailure = "Failed to update the gear item";
    public const string CloudinaryImageUploadFailure = "Failed to upload one or more images to cloudinary";
    public const string CloudinaryImageDeleteFailure = "Failed to delete one or more images from cloudinary";
    public const string GearItemDeleteFailure = "Failed to delete the gear item";
    public const string LeagueImageDeleteFailure = "Failed to delete the league image";
  }
}