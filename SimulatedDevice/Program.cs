using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Config;
using Microsoft.Azure.Amqp.Serialization;
using Microsoft.Azure.Devices.Client;
using Microsoft.ServiceBus;
using Newtonsoft.Json;

namespace SimulatedDevice
{
    internal class Program
    {
        private static DeviceClient _deviceClient;
        private static readonly Random Rand = new Random();
        private static List<Device> _deviceList;

        private class Device
        {
            public string DeviceId { get; set; }
            public string DeviceName { get; set; }
        } 

        public static void Main(string[] args)
        {
            _deviceList = new List<Device>()
            {
                new Device { DeviceId = "e8dca9e8-b665-4e26-8c1a-43ee72e46e07", DeviceName = "Simulated Device 0" },
                new Device { DeviceId = "344ad8ca-9464-4548-9307-bf70bc910d3d", DeviceName = "Simulated Device 1" },
                new Device { DeviceId = "7a813b7e-4d90-499b-9be9-58ac838c8016", DeviceName = "Simulated Device 2" },
                new Device { DeviceId = "b5e7f7e5-fe12-4b79-9643-717f935c611c", DeviceName = "Simulated Device 3" },
                new Device { DeviceId = "e5214eb1-6bc9-423a-a803-5556a6975ddd", DeviceName = "Simulated Device 4" },
                new Device { DeviceId = "9c4b275e-887b-441f-a917-9b2022527b5c", DeviceName = "Simulated Device 5" },
                new Device { DeviceId = "358c03f9-b143-45ac-9a56-d6026abe8665", DeviceName = "Simulated Device 6" },
                new Device { DeviceId = "d264dfd2-b89e-4d4f-819c-33c3c4a0b028", DeviceName = "Simulated Device 7" },
                new Device { DeviceId = "8257de42-6526-4108-9a5c-79902bd43b45", DeviceName = "Simulated Device 8" },
                new Device { DeviceId = "fee15741-975d-46a8-8f6b-55d8fd443c77", DeviceName = "Simulated Device 9" }
            };

            Console.WriteLine("Connecting to IoT Hub, please stand by...");

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
            Console.WriteLine("Starting the RFID device simulation...");

            while (true)
            {
                var random = new Random();
                var chosenDevice = _deviceList[random.Next(0, 9)];

                var rfidData = new RfidData
                {
                    DeviceId = chosenDevice.DeviceId,
                    DeviceName = chosenDevice.DeviceName,
                    Datetime = DateTime.Now,
                    RfidTag = GetRfidTag()
                };

                var messageString = JsonConvert.SerializeObject(rfidData);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await _deviceClient.SendEventAsync(message);

                Console.WriteLine($"Sent message: {messageString}");

                await Task.Delay(100);
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
