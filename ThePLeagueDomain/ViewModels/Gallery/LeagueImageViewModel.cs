using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ThePLeagueDomain.ViewModels.Gallery
{
  public class LeagueImageViewModel : ImageBaseViewModel
  {
    public const string DefaultLeagueImageUrl = "https://res.cloudinary.com/dwsvaiiox/image/upload/v1560115767/movies-place/zcds8f29yreoyvoxuyfe.jpg";
    public string HashTag { get; set; }
    public long? OrderId { get; set; }
  }
}