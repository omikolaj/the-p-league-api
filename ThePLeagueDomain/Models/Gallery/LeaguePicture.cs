namespace ThePLeagueDomain.Models.Gallery
{
  public class LeaguePicture : ImageBase
  {
    public string HashTag { get; set; }
    public bool? Delete { get; set; }
  }
}