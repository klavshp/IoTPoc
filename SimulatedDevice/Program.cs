using System;
using System.Text;
using System.Threading.Tasks;
using Config;
using Microsoft.Azure.Devices.Client;
using Microsoft.ServiceBus;
using Newtonsoft.Json;

namespace SimulatedDevice
{
    internal class Program
    {
        static DeviceClient _deviceClient;
        static readonly Random Rand = new Random();

        public static void Main(string[] args)
        {
            Console.WriteLine("Simulated RFID device\n");
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http;
            _deviceClient = DeviceClient.Create(Config.Config.IotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(Config.Config.DeviceId, Config.Config.DeviceKey));
            
            SendDeviceToCloudMessagesAsync();

            Console.ReadLine();
        }

        private static async void SendDeviceToCloudMessagesAsync()
        {
            while (true)
            {
                var telemetryDataPoint = new RfidData
                {
                    DeviceId = Config.Config.DeviceId,
                    Datetime = DateTime.Now,
                    RfidTag = GetRfidTag()
                };

                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await _deviceClient.SendEventAsync(message);
                Console.WriteLine($"Sent message: {messageString}");

                await Task.Delay(500);
            }
        }
        private static string GetRfidTag()
        {
            var sb = new StringBuilder(26);
            for (var i = 0; i < 25; i++)
            {
                sb.Append(Rand.Next(0, 9));
            }
            return sb.ToString();
        }

    }
}
