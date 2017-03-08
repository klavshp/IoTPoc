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
        public const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=iotpocblobstorageaccount;AccountKey=7S7ebhFyDXnv4wcjRMnZvh6W8EPWaPKKogm+RIYJX61J94HqklPtzrPP0ZDvsYSWc6Wea1ZX41ItvFEMxH7FcA==";
        public const string ServiceBusConnectionString = "Endpoint=sb://iotpocbus.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=BC6GVBl871fa0jjb9yRT0Vwh9WM84TNrw58CodzVvHU=;EntityPath=IoTPocQueue";
        public const string BlobContainer = "iotpocblobcontainer";
        public const string BlockBlobReference = "iotpoc_2";
    }
}