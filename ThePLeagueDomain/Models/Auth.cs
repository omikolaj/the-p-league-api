namespace ThePLeagueDomain.Models
{
  public class Auth
  {
    #region Fields and Properties

    public ApplicationUser User { get; set; }
    public RefreshToken RefreshToken { get; set; }
    public JsonWebToken Jwt { get; set; }

    #endregion
  }
}