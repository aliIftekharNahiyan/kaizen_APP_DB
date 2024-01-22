using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.IO;
using System;
using Azure.Storage.Blobs.Models;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.Extensions.Configuration;
using Azure;
using System.ComponentModel;
using Azure.Storage.Blobs.Specialized;

namespace Kaizen.Web.Services
{
    public class CloudFileStorageService : ICloudFileStorage
    {
        private readonly string _cloudStorageConn;
        private readonly string _cloudContainerName;
        private readonly IConfiguration _configuration;
        public CloudFileStorageService(IConfiguration configurationRoot)
        {
            _configuration = configurationRoot;

            _cloudStorageConn = _configuration["AzureBlobStorage:StorageConnectionString"];
            _cloudContainerName = _configuration["AzureBlobStorage:ContainerName"];

        }
        public async Task<Stream> DownloadFileAsync(string fileName)
        {
            var blobServiceClient = new BlobServiceClient(_cloudStorageConn);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_cloudContainerName);

            var blobClient = blobContainerClient.GetBlobClient(fileName);

            if (await blobClient.ExistsAsync())
            {
                return await blobClient.OpenReadAsync(position: 0);
            }
            
            return null;
        }

        public async Task<string> ReadTextFromFile(string fileName)
        {
            var blobServiceClient = new BlobServiceClient(_cloudStorageConn);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_cloudContainerName);
            var blobClient2 = blobContainerClient.GetBlockBlobClient(fileName);

            if (blobClient2.Exists())
            {
                BlobDownloadInfo download = blobClient2.Download();
                var content = download.Content;
                using (var streamReader = new StreamReader(content))
                {
                    return streamReader.ReadToEnd();
                }
            }

            return string.Empty;
        }


        public async Task<BlobClient> UploadFileAsync(IFormFile file, string fileName)
        {

            var blobServiceClient = new BlobServiceClient(_cloudStorageConn);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_cloudContainerName);

            if (fileName == null)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            }
            var blobClient = blobContainerClient.GetBlobClient(fileName);
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient;
        }

        public async Task<BlobClient> UploadFileAsync(string htmlContent, string fileName)
        {

            var blobServiceClient = new BlobServiceClient(_cloudStorageConn);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_cloudContainerName);

            var blobClient = blobContainerClient.GetBlobClient(fileName);

            byte[] bytes = null;
            using (var ms = new MemoryStream())
            {
                using (TextWriter tw = new StreamWriter(ms))
                {
                    tw.Write(htmlContent);
                    tw.Flush();
                    ms.Position = 0;
                    bytes = ms.ToArray();

                    await blobClient.UploadAsync(ms, true);
                }

            }

            return blobClient;
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var blobServiceClient = new BlobServiceClient(_cloudStorageConn);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_cloudContainerName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            await blobClient.DeleteIfExistsAsync();
        }
    }
}
