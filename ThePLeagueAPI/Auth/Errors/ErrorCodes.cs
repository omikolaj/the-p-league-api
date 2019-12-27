using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ThePLeagueAPI.Auth.Errors
{
    public class ErrorCodes
    {
        public const string Login = "login";
        public const string Logout = "logout";
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
        public const string PasswordUpdate = "password_update";
        public const string SendEmail = "send_email";
        public const string SportTypeAdd = "sport_type_add";
        public const string SportTypeUpdate = "sport_type_update";
        public const string SportTypeDelete = "sport_type_delete";
        public const string LeagueAdd = "league_add";
        public const string LeagueUpdate = "league_update";
        public const string LeagueDelete = "league_delete";
        public const string TeamAdd = "team_add";
        public const string TeamUpdate = "team_update";
        public const string TeamDelete = "team_delete";
        public const string TeamAssign = "team_assign";
        public const string TeamUnassign = "team_unassign";
    }
}