using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThePLeagueDomain.Models;

namespace ThePLeagueDataCore
{
  public class ThePLeagueContext : IdentityDbContext<BaseUser>
  {
    public ThePLeagueContext(DbContextOptions<ThePLeagueContext> options) : base(options) { }
    public ThePLeagueContext() { }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
  }
}