using MessageProcessing.ReadConfig;
using Microsoft.Extensions.Configuration;

namespace Weather_Monitoring.ReadConfig
{
    public class ConfigReader
    {
        public static SignalRConfig ReadSignalRConfig()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var SignalRUrl = configuration["SignalRConfig:SignalRUrl"];

            return new SignalRConfig(SignalRUrl);
        }
    }
}