namespace ThePLeagueDomain.ViewModels.Merchandise
{
  public class GearImageViewModel : ImageBaseViewModel
  {
    public const string DefaultGearItemImageUrl = "https://res.cloudinary.com/dwsvaiiox/image/upload/v1561075265/movies-place/whznw3cbufax7zpaiiv1.jpg";
    public long? GearItemId { get; set; }
    public bool Delete { get; set; }
    public GearItemViewModel GearItem { get; set; }
  }
}