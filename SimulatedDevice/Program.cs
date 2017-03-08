using System;
using System.Globalization;
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

        public static void Main(string[] args)
        {
            Console.WriteLine("Simulated device\n");
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http;
            _deviceClient = DeviceClient.Create(Config.Config.IotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(Config.Config.DeviceId, Config.Config.DeviceKey));

            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();
        }

        private static async void SendDeviceToCloudMessagesAsync()
        {
            double avgWindSpeed = 10; // m/s
            Random rand = new Random();

            while (true)
            {
                var currentWindSpeed = avgWindSpeed + rand.NextDouble() * 4 - 2;

                var telemetryDataPoint = new RfidData
                {
                    DeviceId = Config.Config.DeviceId,
                    Datetime = DateTime.Now,
                    RfidTag = currentWindSpeed.ToString(CultureInfo.InvariantCulture)
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);

                string levelValue;

                if (rand.NextDouble() > 0.7)
                {
                    messageString = "This is a critical message";
                    levelValue = "critical";
                }
                else
                {
                    levelValue = "normal";
                }

                var message = new Message(Encoding.ASCII.GetBytes(messageString));
                message.Properties.Add("level", levelValue);

                await _deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sent message: {1}", DateTime.Now, messageString);

                await Task.Delay(1000);
            }
        }

        private static async void SendDeviceToCloudInteractiveMessagesAsync()
        {
            while (true)
            {
                var interactiveMessageString = "Alert message!";
                var interactiveMessage = new Message(Encoding.ASCII.GetBytes(interactiveMessageString));
                interactiveMessage.Properties["messageType"] = "interactive";
                interactiveMessage.MessageId = Guid.NewGuid().ToString();

                await _deviceClient.SendEventAsync(interactiveMessage);
                Console.WriteLine("{0} > Sending interactive message: {1}", DateTime.Now, interactiveMessageString);

                Task.Delay(10000).Wait();
            }
        }



        private static async void SendDeviceToCloudMessagesAsync1()
        {
            double avgWindSpeed = 10; // m/s
            double avgTemperature = 20;
            var rand = new Random();

            while (true)
            {
                var currentWindSpeed = Math.Round(avgWindSpeed + rand.NextDouble() * 4 - 2, 2);
                var currentTemperature = Math.Round(avgTemperature + rand.NextDouble(), 1);//* 4 - 2;

                var telemetryDataPoint = new
                {
                    deviceId = Config.Config.DeviceId,
                    windSpeed = currentWindSpeed,
                    temperature = currentTemperature
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));
                await _deviceClient.SendEventAsync(message);
                Console.WriteLine($"{DateTime.Now} > Sending message: {messageString}");

                Task.Delay(1000).Wait();
            }
        }
    }
}
