namespace ThePLeagueDomain.ViewModels.Merchandise
{
    public class GearImageViewModel : ImageBaseViewModel
    {
        public const string DefaultGearItemImageUrlSmall = "https://res.cloudinary.com/dkbelxhih/image/upload/f_auto,q_70,w_360/v1581902163/pleague/gear_placeholder_flvhmb.jpg";
        public const string DefaultGearItemImageUrlMedium = "https://res.cloudinary.com/dkbelxhih/image/upload/f_auto,q_70/v1581902163/pleague/gear_placeholder_flvhmb.jpg";
        public const string DefaultGearItemImageUrlBig = "https://res.cloudinary.com/dkbelxhih/image/upload/f_auto,q_70/v1581902163/pleague/gear_placeholder_flvhmb.jpg";
        public long? GearItemId { get; set; }
        public bool Delete { get; set; }
        public GearItemViewModel GearItem { get; set; }
    }
}