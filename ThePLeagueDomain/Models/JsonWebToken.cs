namespace ThePLeagueDomain.Models
{
  public class JsonWebToken
  {
    #region Fields and Properties

    public string UserId { get; set; }
    public string Token { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; }

    #endregion
  }
}