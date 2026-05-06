using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobProject.Services;

public class ContainerService(BlobServiceClient blobClient) : IContainerService
{
    public async Task<List<string>> GetAllContainerAndBlobs()
    {
        List<string> result = new();
        
        await foreach (BlobContainerItem blobContainerItem in blobClient.GetBlobContainersAsync())
        {
            string containerName = blobContainerItem.Name;
            result.Add($"Container: {containerName}");
            
            BlobContainerClient blobContainerClient = blobClient.GetBlobContainerClient(containerName);
            await foreach (BlobItem blobItem in blobContainerClient.GetBlobsAsync())
            {
                result.Add($"  - {blobItem.Name}");
            }
        }
        
        return result;
    }

    public async Task<List<string>> GetAllContainers()
    {
        List<string> containerNames = new();
        await foreach (BlobContainerItem blobContainerItem in blobClient.GetBlobContainersAsync())
        {
            containerNames.Add(blobContainerItem.Name);
        }
        return containerNames;
    }

    public async Task CreateContainer(string containerName)
    {
        BlobContainerClient blobContainerClient = blobClient.GetBlobContainerClient(containerName);
        await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
    }

    public async Task DeleteContainer(string containerName)
    {
        BlobContainerClient blobContainerClient = blobClient.GetBlobContainerClient(containerName);
        await blobContainerClient.DeleteIfExistsAsync();
    }
}