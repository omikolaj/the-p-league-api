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
                options.CacheProfiles.Add("ETag", new CacheProfile
                {
                    Duration = 0,
                    Location = ResponseCacheLocation.None,
                    NoStore = false,
                    VaryByHeader = "Accept-Language, Accept-Encoding"
                });
            });

            return services;
        }
        
    }
}
