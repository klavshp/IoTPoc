using System;
using Microsoft.ServiceBus.Messaging;

namespace ProcessDeviceToCloudMessages
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            StoreEventProcessor.StorageConnectionString = Config.Config.StorageConnectionString;
            StoreEventProcessor.ServiceBusConnectionString = Config.Config.ServiceBusConnectionString;

            // Setup the EventProcessorHost
            var eventProcessorHost = new EventProcessorHost(
                Guid.NewGuid().ToString(), 
                Config.Config.IotHubD2CEndpoint, 
                EventHubConsumerGroup.DefaultGroupName, 
                Config.Config.IoTHubConnectionString,
                Config.Config.StorageConnectionString,
                "messages-events");

            Console.WriteLine("Registering EventProcessor...");

            eventProcessorHost.RegisterEventProcessorAsync<StoreEventProcessor>().Wait();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();

            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}
