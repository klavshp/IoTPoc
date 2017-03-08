namespace Config
{
    public class Config
    {
        // IoT Hub
        public const string IoTHubConnectionString = "HostName=KlavsGetStartedWithIoTHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=bA7dlhxUWj1OZuADkxDNFGbvbaICsOWA84vdUZ5pnrM=";
        public const string IotHubUri = "KlavsGetStartedWithIoTHub.azure-devices.net";
        public const string IotHubD2CEndpoint = "messages/events";

        // Simulated Device
        public const string DeviceId = "SimulatedRfidReader";
        public const string DeviceKey = "J3d6ijVFht0kEOTtfyg9KD/fRn4KsOKOJfBemewHsP0=";

        // ConnectionStrings for ServiceBus and Blob Storage
        public const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=klavsstorageaccount;AccountKey=rRPjNpofS3c+1ZHVIwwujbE9gAuZfKCPrOIlBjuBvqvPdQN26QYHfUrnppDRoEBGTv6BgGtPH++Nd54xskWzag==";
        public const string ServiceBusConnectionString = "Endpoint=sb://klavssb.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=/9Mi8zw2Uh5L/nuM9ppYDWiZ74fyteNNnCy0PJ8ugA4=;EntityPath=D2CTutorial";
        public const string BlobContainer = "d2ctutorial";
        public const string BlockBlobReference = "iothubd2c_2";
    }
}