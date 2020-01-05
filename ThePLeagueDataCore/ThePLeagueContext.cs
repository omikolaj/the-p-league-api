using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThePLeagueDataCore.Configurations.Merchandise;
using ThePLeagueDataCore.Configurations.Schedule;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Gallery;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.Models.TeamSignUp;

namespace ThePLeagueDataCore
{
    public class ThePLeagueContext : IdentityDbContext<ApplicationUser>
    {
        public ThePLeagueContext(DbContextOptions<ThePLeagueContext> options) : base(options) { }
        public ThePLeagueContext() { }
        public DbSet<GearItem> GearItems { get; set; }
        public DbSet<GearImage> GearImages { get; set; }
        public DbSet<GearSize> GearSizes { get; set; }
        public DbSet<LeagueImage> LeagueImages { get; set; }
        public DbSet<TeamSignUpForm> TeamSignUpForms { get; set; }
        public DbSet<Contact> TeamsContact { get; set; }
        public DbSet<PreOrder> PreOrders { get; set; }
        public DbSet<PreOrderContact> PreOrderContacts { get; set; }
        public DbSet<GameDay> GameDays { get; set; }
        public DbSet<GameTime> GameTimes { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<LeagueSessionSchedule> LeagueSessions { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<SportType> SportTypes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<MatchResult> MatchResults { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new GearImageConfiguration(builder.Entity<GearImage>());
            new GearItemConfiguration(builder.Entity<GearItem>());
            new GearSizeConfiguration(builder.Entity<GearSize>());
            new SportTypeConfiguration(builder.Entity<SportType>());
            new LeagueConfiguration(builder.Entity<League>());
            new TeamConfiguration(builder.Entity<Team>());            
            new MatchesConfiguration(builder.Entity<Match>());
            new TeamSessionConfiguration(builder.Entity<TeamSession>());
            new MatchResultsConfigration(builder.Entity<MatchResult>());
        }
    }
}