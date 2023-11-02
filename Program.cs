using Weather_Monitoring.ReadConfig;
namespace SignalREvent
{
    class Program
    {
        static void Main(string[] args)
        {
            var signalRConfig = ConfigReader.ReadSignalRConfig();
            var signalRClient = new SignalRClient(signalRConfig.SignalRUrl);

            try
            {
                signalRClient.StartAsync().GetAwaiter().GetResult();
                Console.WriteLine("SignalR client connected. Press 'Q' to quit the application.");
                while (true)
                {
                    var eventData = signalRClient.AwaitEvent();

                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(intercept: true).Key;
                        if (key == ConsoleKey.Q)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                signalRClient.StopAsync().GetAwaiter().GetResult();
            }
        }
    }
}