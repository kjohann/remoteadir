using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionApp1
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddEnvironmentVariables();

            var environmentName = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");
            if (environmentName == null ||
                environmentName.Equals("Production", StringComparison.InvariantCultureIgnoreCase) == false)
            {
                configBuilder.AddJsonFile("local.settings.json", true);
            }

            builder.Services.AddLogging();
        }
    }
}