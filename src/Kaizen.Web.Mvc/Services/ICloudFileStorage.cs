using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Kaizen.Web.Services
{
    public interface ICloudFileStorage
    {
        Task<BlobClient> UploadFileAsync(IFormFile file, string fileName);
        Task<BlobClient> UploadFileAsync(string htmlContent, string fileName);
        Task<Stream> DownloadFileAsync(string fileName);
        Task<string> ReadTextFromFile(string fileName);
        Task DeleteFileAsync(string fileName);

    }
}
