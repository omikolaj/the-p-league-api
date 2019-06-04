using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThePLeagueDataCore.Configurations.Merchandise;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Merchandise;

namespace ThePLeagueDataCore
{
  public class ThePLeagueContext : IdentityDbContext<ApplicationUser>
  {
    public ThePLeagueContext(DbContextOptions<ThePLeagueContext> options) : base(options) { }
    public ThePLeagueContext() { }
    public DbSet<GearItem> GearItems { get; set; }
    public DbSet<GearImage> GearImages { get; set; }
    public DbSet<GearSize> GearSizes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      new GearImageConfiguration(builder.Entity<GearImage>());
      new GearItemConfiguration(builder.Entity<GearItem>());
      new GearSizeConfiguration(builder.Entity<GearSize>());
    }
  }
}