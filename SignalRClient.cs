using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;
namespace SignalREvent
{
    public class SignalRClient
    {
        private readonly Microsoft.AspNetCore.SignalR.Client.HubConnection _hubConnection;

        public SignalRClient(string signalRUrl)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(signalRUrl)
                .Build();

            _hubConnection.On<string>("ReceiveMessage", (message) =>
            {
                Console.WriteLine($"Received message: {message}");
            });
        }

        public async Task StartAsync()
        {
            try
            {
                await _hubConnection.StartAsync();
                Console.WriteLine("Connected to SignalR hub.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to SignalR hub: {ex.Message}");
            }
        }

        public async Task StopAsync()
        {
            try
            {
                await _hubConnection.StopAsync();
                Console.WriteLine("Connection to SignalR hub stopped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping SignalR hub connection: {ex.Message}");
            }
        }

        public async Task<string> AwaitEvent()
        {
            var completionSource = new TaskCompletionSource<string>();
            _hubConnection.On<string>("YourEventName", eventData =>
            {
                completionSource.SetResult(eventData);
            });
            return await completionSource.Task;
        }
    }
}