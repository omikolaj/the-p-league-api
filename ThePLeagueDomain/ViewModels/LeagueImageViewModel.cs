using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ThePLeagueDomain.ViewModels
{
  public class LeagueImageViewModel : ImageBaseViewModel
  {
    public const string DefaultLeagueImageUrl = "https://res.cloudinary.com/dwsvaiiox/image/upload/v1560115767/movies-place/zcds8f29yreoyvoxuyfe.jpg";
    public long? Id { get; set; }
    public string HashTag { get; set; }
  }
}