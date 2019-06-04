using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThePLeagueDomain.Models.Merchandise;

namespace ThePLeagueDataCore.Configurations.Merchandise
{
  public class GearImageConfiguration
  {
    public GearImageConfiguration(EntityTypeBuilder<GearImage> model)
    {
      model.HasOne(gearImage => gearImage.GearItem)
            .WithMany(gearItem => gearItem.Images)
            .HasForeignKey(gearImage => gearImage.GearItemId);

      //Seed Data
      List<GearImage> gearImages = new List<GearImage>();
      for (int i = 0; i < 11; i++)
      {
        gearImages.AddRange(new GearImage[]{
          new GearImage()
          {
            Id = i + 11231,
            GearItemId = i + 1,
            Name = "wow",
            Url = "https://via.placeholder.com/300.png/09f/fff",
            Small = "https://via.placeholder.com/300.png/09f/fff",
            Medium = "https://via.placeholder.com/300.png/09f/fff",
            Big = "https://via.placeholder.com/300.png/09f/fff",
            Size = 19392,
            Type = "png"
          },
          new GearImage(){
            Id = i + 2322,
            GearItemId = i + 1,
            Name = "wowwee",
            Url = "https://via.placeholder.com/300.png/09f/fff",
            Small = "https://via.placeholder.com/300.png/09f/fff",
            Medium = "https://via.placeholder.com/300.png/09f/fff",
            Big = "https://via.placeholder.com/300.png/09f/fff",
            Size = 19392,
            Type = "png"
          },
          new GearImage(){
            Id = i + 34323,
            GearItemId = i + 1,
            Name = "wowwaw",
            Url = "https://via.placeholder.com/300.png/09f/fff",
            Small = "https://via.placeholder.com/300.png/09f/fff",
            Medium = "https://via.placeholder.com/300.png/09f/fff",
            Big = "https://via.placeholder.com/300.png/09f/fff",
            Size = 19392,
            Type = "png"
          }
        });

      }
      model.HasData(gearImages);
    }
  }
}