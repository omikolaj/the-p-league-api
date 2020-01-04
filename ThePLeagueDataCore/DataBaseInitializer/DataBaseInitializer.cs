using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Schedule;

namespace ThePLeagueDataCore.DataBaseInitializer
{
    public class DataBaseInitializer
    {
        #region Methods

        public static void Initialize(IServiceProvider serviceProvider)
        {
            IdentitySeedData.Populate(serviceProvider).Wait();
            SeedData.Populate(serviceProvider);
        }
        
        #endregion
    }
}