using Azure.Storage.Blobs;
using AzureBlobProject.Models;

namespace AzureBlobProject.Services;

public class BlobService(BlobServiceClient blobClient) : IBlobService
{
    public async Task<List<string>> GetAllBlobs(string containerName)
    {
        BlobContainerClient blobContainerClient = blobClient.GetBlobContainerClient(containerName);
        var blobs = blobContainerClient.GetBlobsAsync();
        List<string> blobNames = new();
        await foreach(var blobItem in blobs)
        {
            blobNames.Add(blobItem.Name);
        }
        return blobNames;
        

    }

    public Task<List<BlobModel>> GetAllBlobsWithUri(string containerName)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetBlob(string name, string containerName)
    {
        BlobContainerClient blobContainerClient = blobClient.GetBlobContainerClient(containerName);
        var blobs = blobContainerClient.GetBlobClient(name);
        if (blobs != null)
        {
            return blobs.Uri.AbsoluteUri;
        }

        return "";
    }

    public async Task<bool> CreateBlob(string name, string containerName, IFormFile file, BlobModel blobModel)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteBlob(string name, string containerName)
    {
        BlobContainerClient blobContainerClient = blobClient.GetBlobContainerClient(containerName);
        var blobs = blobContainerClient.GetBlobClient(name);
        return await blobs.DeleteIfExistsAsync();

    }
}