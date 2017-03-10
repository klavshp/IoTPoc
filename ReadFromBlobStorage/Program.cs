using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Config;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;

namespace ReadFromBlobStorage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string text;
            var storageAccount = CloudStorageAccount.Parse(Config.Config.StorageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(Config.Config.BlobContainer);
            container.CreateIfNotExists();
            var blockBlob = container.GetBlockBlobReference(Config.Config.BlockBlobReference);

            Console.WriteLine("Preparing to download blob data...");

            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
                text = Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            foreach (var item in JsonConvert.DeserializeObject<List<RfidData>>(ToJsonArray(text)))
            {
                Console.WriteLine($"{item.DeviceId}   {item.DeviceName}   {item.Datetime}   {item.RfidTag}");
            }

            Console.WriteLine("Press Enter to quit.");

            Console.ReadLine();
        }

        private static string ToJsonArray(string input)
        {
            return "[" + input.Replace("}", "},") + "]";
        }
    }
}
