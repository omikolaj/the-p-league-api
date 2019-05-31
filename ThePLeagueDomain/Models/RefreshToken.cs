using System;

namespace ThePLeagueDomain.Models
{
  public class RefreshToken
  {
    #region Fields and Properties

    public string Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool Active => DateTime.UtcNow <= Expires;

    #endregion

    #region Constructor

    public RefreshToken(string token, DateTime expires, string userId)
    {
      this.Token = token;
      this.Expires = expires;
      this.UserId = userId;
    }

    #endregion
  }
}