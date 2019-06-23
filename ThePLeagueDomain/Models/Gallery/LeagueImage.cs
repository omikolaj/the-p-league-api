namespace ThePLeagueDomain.Models.Gallery
{
  public class LeagueImage : ImageBase
  {
    public long? Id { get; set; }
    public string HashTag { get; set; }
    public bool? Delete { get; set; }
    public long? OrderId { get; set; }
  }
}