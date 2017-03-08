namespace Config
{
    public class Config
    {
        // IoT Hub
        public const string IoTHubConnectionString = "HostName=IoTPocIoTHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=JB6VfRhDkxZHxxCQkzqbOszaqJxEG2io/qo/vh3lkY0=";
        public const string IotHubUri = "IoTPocIoTHub.azure-devices.net";
        public const string IotHubD2CEndpoint = "messages/events";      // Default endpoint

        // Simulated Device
        public const string DeviceId = "SimulatedRfidReader";
        public const string DeviceKey = "UGWH1g1MxiznfT6EZ6Ej1MwhWZe4KeQGjtuunrEdukg=";

        // ConnectionStrings for ServiceBus and Blob Storage
        public const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=klavsstorageaccount;AccountKey=rRPjNpofS3c+1ZHVIwwujbE9gAuZfKCPrOIlBjuBvqvPdQN26QYHfUrnppDRoEBGTv6BgGtPH++Nd54xskWzag==";
        public const string ServiceBusConnectionString = "Endpoint=sb://klavssb.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=/9Mi8zw2Uh5L/nuM9ppYDWiZ74fyteNNnCy0PJ8ugA4=;EntityPath=D2CTutorial";
        public const string BlobContainer = "d2ctutorial";
        public const string BlockBlobReference = "iothubd2c_2";
    }
}