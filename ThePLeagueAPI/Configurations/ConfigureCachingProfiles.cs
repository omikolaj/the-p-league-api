using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThePLeagueAPI.Configurations
{
    public static class ConfigureCachingProfiles
    {
        public static IServiceCollection ConfigureHttpCachingProfiles(this IServiceCollection services)
        {
            services.AddMvc(options => 
            {
                options.CacheProfiles.Add("SixHours", new CacheProfile
                {
                    Duration = 21600,
                    Location = ResponseCacheLocation.Any,
                    NoStore = false,
                    VaryByHeader = "Accept-Encoding"
                });

                options.CacheProfiles.Add("OneHour", new CacheProfile
                {
                    Duration = 3600,
                    Location = ResponseCacheLocation.Any,
                    NoStore = false,
                    VaryByHeader = "Accept-Encoding"
                });
            });

            return services;
        }
        
    }
}
