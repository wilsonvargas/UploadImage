using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadImagesSample.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(BlobServices))]
namespace UploadImagesSample.Services
{
    public class BlobServices : IBlobServices
    {
        public async Task<string> Upload(string filePath)
        {
            CloudBlobClient blobClient = CloudStorageAccount.Parse(Universal.StorageConnectionString).CreateCloudBlobClient();
            CloudBlobContainer containerReference = blobClient.GetContainerReference(Universal.NameContainerReference);
            CloudBlockBlob blobReference = containerReference.GetBlockBlobReference(filePath.Split('/').Last());
            blobReference.Properties.ContentType = "image/jpeg";
            using (var fileStream = File.OpenRead(filePath))
            {
                await blobReference.UploadFromStreamAsync(fileStream);
            }
            return blobReference.Uri.AbsoluteUri;
        }
    }
}
