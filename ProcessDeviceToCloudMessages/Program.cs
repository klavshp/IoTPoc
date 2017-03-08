using System;
using Microsoft.ServiceBus.Messaging;

namespace ProcessDeviceToCloudMessages
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var iotHubConnectionString = Config.Config.IoTHubConnectionString;
            var iotHubD2cEndpoint = Config.Config.IotHubD2CEndpoint;
            StoreEventProcessor.StorageConnectionString = Config.Config.StorageConnectionString;
            StoreEventProcessor.ServiceBusConnectionString = Config.Config.ServiceBusConnectionString;
            var eventProcessorHostName = Guid.NewGuid().ToString();
            var leaseContainerName = "messages-events";

            var eventProcessorHost = new EventProcessorHost(eventProcessorHostName, iotHubD2cEndpoint, EventHubConsumerGroup.DefaultGroupName, iotHubConnectionString, StoreEventProcessor.StorageConnectionString, leaseContainerName);

            Console.WriteLine("Registering EventProcessor...");
            eventProcessorHost.RegisterEventProcessorAsync<StoreEventProcessor>().Wait();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();

            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}
