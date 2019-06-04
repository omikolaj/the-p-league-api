using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThePLeagueDomain.Models.Merchandise;

namespace ThePLeagueDataCore.Configurations.Merchandise
{
  public class GearSizeConfiguration
  {
    public GearSizeConfiguration(EntityTypeBuilder<GearSize> model)
    {
      model.HasOne(gearSize => gearSize.GearItem)
            .WithMany(gearItem => gearItem.Sizes)
            .HasForeignKey(gearSize => gearSize.GearItemId);

      //Seed Data
      List<GearSize> gearSizes = new List<GearSize>();
      for (int i = 0; i < 11; i++)
      {
        gearSizes.AddRange(new GearSize[]{
            new GearSize()
      {
        Id = i + 102,
        GearItemId = i + 1,
        Size = Size.L,
        Available = true,
        Color = "warn"
      },
      new GearSize()
      {
        Id = i + 1334,
        GearItemId = i + 1,
        Size = Size.XL,
        Available = false,
        Color = "warn"
      }, new GearSize()
      {
        Id = 44 + i,
        GearItemId = i + 1,
        Size = Size.XXL,
        Available = true,
        Color = "warn"
      }
      , new GearSize()
      {
        Id = i + 45678,
        GearItemId = i + 1,
        Size = Size.M,
        Available = false,
        Color = "warn"
      }
      , new GearSize()
      {
        Id = i + 9099,
        GearItemId = i + 1,
        Size = Size.S,
        Available = false,
        Color = "warn"
      }
      , new GearSize()
      {
        Id = i + 85843,
        GearItemId = i + 1,
        Size = Size.XS,
        Available = true,
        Color = "warn"
      }
      });
      }
      model.HasData(gearSizes);
    }
  }
}