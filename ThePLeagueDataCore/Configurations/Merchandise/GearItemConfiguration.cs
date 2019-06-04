using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThePLeagueDomain.Models.Merchandise;

namespace ThePLeagueDataCore.Configurations.Merchandise
{
  public class GearItemConfiguration
  {
    public GearItemConfiguration(EntityTypeBuilder<GearItem> model)
    {
      model.HasMany(gearItem => gearItem.Images)
            .WithOne(gearImage => gearImage.GearItem);

      model.HasMany(gearItem => gearItem.Sizes)
            .WithOne(gearSize => gearSize.GearItem);

      //Seed Data
      string[] gearItemNames = new string[]{
        "T-shirt",
        "Pants",
        "Hat",
        "Jorts",
        "Long Sleeve",
        "Shoes",
        "Trousers",
        "Zip-Up",
        "Track Jacket",
        "Cut off",
        "Suprirse"
      };
      List<GearItem> gearItems = new List<GearItem>();

      for (int i = 0; i < 11; i++)
      {
        gearItems.AddRange(new GearItem[]{
            new GearItem(){
              Id = i + 1,
              InStock = i % 2 == 0 ? true : false,
              Name = gearItemNames[i],
              Price = 25 + i
            }
          });
      }

      model.HasData(gearItems);
    }
  }
}