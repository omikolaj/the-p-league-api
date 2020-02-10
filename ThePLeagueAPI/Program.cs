using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ThePLeagueAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Fatal error occured when calling CreateWebHostBuilder");
            }

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(@"D:\startuplogs.txt")
            .MinimumLevel.Verbose()
            .CreateLogger();

            try
            {
                return WebHost.CreateDefaultBuilder(args)                    
                  .ConfigureAppConfiguration((ContextBoundObject, config) =>
                  {
                      if (ContextBoundObject.HostingEnvironment.IsProduction())
                      {
                          var builtConfig = config.Build();

                          var azureServiceTokenProvider = new AzureServiceTokenProvider();
                          var keyVaultClient = new KeyVaultClient(
                        new KeyVaultClient.AuthenticationCallback(
                          azureServiceTokenProvider.KeyVaultTokenCallback
                        )
                      );
                          config.AddAzureKeyVault(
                        $@"https://{builtConfig["KeyVaultName"]}.vault.azure.net/",
                        keyVaultClient,
                        new DefaultKeyVaultSecretManager()
                      );

                      }
                  })
                .UseStartup<Startup>();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host builder error");

                throw;
            }
            finally
            {
                Log.Information("finished starting up");
            }

            
        }
            
    }
}
