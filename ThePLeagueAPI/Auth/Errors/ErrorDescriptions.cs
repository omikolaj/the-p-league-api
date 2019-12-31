using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ThePLeagueAPI.Auth.Errors
{
    public class ErrorDescriptions
    {
        public const string LoginFailure = "Invalid username or password";
        public const string RefreshTokenDeleteFailure = "Failed to delete the refresh token";
        public const string RefreshTokenFailure = "Invalid or expired refresh token";
        public const string UserNotFoundFailure = "This user was not found in the database";
        public const string GearItemUpdateFailure = "Failed to update the gear item";
        public const string CloudinaryImageUploadFailure = "Failed to upload one or more images to cloudinary";
        public const string CloudinaryImageDeleteFailure = "Failed to delete one or more images from cloudinary";
        public const string GearItemDeleteFailure = "Failed to delete the gear item";
        public const string GearItemFindFailure = "Failed to failed specified the gear item";
        public const string LeagueImageDeleteFailure = "Failed to delete the league image";
        public const string LeagueImageSaveOrderFailure = "Failed to save league images order, on one or more images";
        public const string PasswordUpdateFailure = "Failed to update the password";
        public const string SendEmailFailure = "Failed to send the email";
        public const string SportTypeAddFailure = "Failed to add the new sport type";
        public const string SportTypeUpdateFailure = "Failed to update the sport type";
        public const string SportTypeDeleteFailure = "Failed to delete the sport type";
        public const string SportTypeNotFound = "Failed to find the specified sport type";
        public const string LeagueAddFailure = "Failed to add the new league";
        public const string LeagueUpdateFailure = "Failed to update the league";
        public const string LeagueDeleteFailure = "Failed to delete the league";
        public const string LeagueNotFound = "Failed to find the specified league";
        public const string TeamAddFailure = "Failed to add the new team";
        public const string TeamUpdateFailure = "Failed to update the team";
        public const string TeamDeleteFailure = "Failed to delete the team";
        public const string TeamNotFound = "Failed to find the specified team";
        public const string TeamAssign = "Failed to assign teams to the specified league";
        public const string TeamUnassign = "Failed to unassign team from the specified league";
        public const string GetUnassignedTeamsFailure = "Failed to retrieve all of the teams that are not assigned to a league";
        public const string ActiveSessionsInfoFailure = "Failed to retrieve one or session schedules for the specified league ids";
        public const string PublishingNewSessionsFailure = "Failed to publish new sessions";
    }
}
