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
    public const string GearItemUpdate = "gear_item_update";
    public const string CloudinaryUpload = "cloudinary_upload";
    public const string CloudinaryDelete = "cloudinary_delete";
    public const string GearItemDelete = "gear_item_delete";
    public const string LeagueImageDelete = "league_image_delete";
    public const string GearItemNotFound = "gear_item_not_found";
    public const string LeagueImageNotFound = "league_image_not_found";
    public const string LeagueImageOrder = "league_image_order";
  }
}