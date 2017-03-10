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
        private static DeviceClient _deviceClient;
        private static readonly Random Rand = new Random();

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting the simulated RFID device...");
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http;
            _deviceClient = DeviceClient.Create(
                Config.Config.IotHubUri, 
                new DeviceAuthenticationWithRegistrySymmetricKey(Config.Config.DeviceName, Config.Config.DeviceKey)
                );
            
            SendDeviceToCloudMessagesAsync();

            Console.ReadLine();
        }

        private static async void SendDeviceToCloudMessagesAsync()
        {
            while (true)
            {
                var rfidData = new RfidData
                {
                    DeviceId = Config.Config.DeviceId,
                    DeviceName = Config.Config.DeviceName,
                    Datetime = DateTime.Now,
                    RfidTag = GetRfidTag()
                };

                var messageString = JsonConvert.SerializeObject(rfidData);
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
