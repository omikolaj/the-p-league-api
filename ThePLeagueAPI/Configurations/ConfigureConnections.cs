using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThePLeagueDataCore;
using ThePLeagueDomain.DbInfo;

namespace ThePLeagueAPI.Configurations
{
  public static class ConfigureConnections
  {
    public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)
    {
      string connection = configuration.GetConnectionString("DefaultConnection");


      //string connection = configuration.GetConnectionString("PostgreSQL");

      //Add ThePLeagueContext to the DI container          
      services.AddDbContext<ThePLeagueContext>(options => 
      {
          options.UseSqlServer(connection);    
      }, ServiceLifetime.Scoped);

      services.AddSingleton(new DbInfo(connection));

      return services;
    }
  }
}