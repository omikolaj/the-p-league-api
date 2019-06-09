namespace ThePLeagueAPI.Auth.Jwt
{
  public static class Constants
  {
    public static class Roles
    {
      public const string AdminRole = "admin";
      public const string UserRole = "user";
    }
    public static class Strings
    {
      public const string TokenName = "jwt";

      public static class JwtClaimIdentifiers
      {
        public const string Role = "role", Id = "id", UserName = "username", Permission = "permission";
      }

      public static class JwtClaims
      {
        public const string ApiAccess = "Admin";
      }
    }
  }
}