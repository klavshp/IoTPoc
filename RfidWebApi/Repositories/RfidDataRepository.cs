using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Config;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;

namespace RfidWebApi.Repositories
{
    public class RfidDataRepository
    {
        public IEnumerable<RfidData> GetAllRfidData()
        {
            string text;

            var storageAccount = CloudStorageAccount.Parse(Config.Config.StorageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(Config.Config.BlobContainer);
            container.CreateIfNotExists();
            var blockBlob = container.GetBlockBlobReference(Config.Config.BlockBlobReference);

            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
                text = Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            return JsonConvert.DeserializeObject<List<RfidData>>(ToJsonArray(text)).Select(item => new RfidData
            {
                DeviceId = item.DeviceId,
                DeviceName = item.DeviceName,
                Datetime = item.Datetime,
                RfidTag = item.RfidTag
            }).ToList();
        }

        private static string ToJsonArray(string input)
        {
            return "[" + input.Replace("}", "},") + "]";
        }


    }
}