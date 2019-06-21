using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ThePLeagueDomain.Models
{
  public class ImageBase
  {
    #region Fields and Properties
    public string Name { get; set; }
    public long? Size { get; set; }
    public string Type { get; set; }
    public string Url { get; set; }
    public string Small { get; set; }
    public string Medium { get; set; }
    public string Big { get; set; }
    public string CloudinaryPublicId { get; set; }
    public string Format { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string ResourceType { get; set; }

    #endregion
  }
}