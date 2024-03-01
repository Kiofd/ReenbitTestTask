using Azure.Storage.Blobs;
using Test_Task_.Net_Reenbit_Masksym_Sheremeta.Models;

namespace Test_Task_.Net_Reenbit_Masksym_Sheremeta.Services;

public class FileService : IFileService
{   
    private readonly string _connectionString;
    private readonly string _conteiner;
    public FileService(IConfiguration configuration)
    {
        _connectionString = configuration.GetValue<string>("AzureBlobStorage:ConnectionString");
        _conteiner = configuration.GetValue<string>("AzureBlobStorage:ContainerName");
    }
    public async Task<string> UploadFileAsync(Blob data)
    {
        string fileName = string.Join('_', Guid.NewGuid(), data.File.FileName); //_ is a seperator

        SasToken token = new SasToken(_connectionString, _conteiner);

        BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(token.GenerateSasToken()));

        BlobContainerClient blobContainer = blobServiceClient.GetBlobContainerClient(_conteiner);

        BlobClient blobClient = blobContainer.GetBlobClient(fileName);

        var memoryStream = new MemoryStream();
        await data.File.CopyToAsync(memoryStream);

        memoryStream.Position = 0;

        await blobClient.UploadAsync(memoryStream);

        await blobClient.SetMetadataAsync(new Dictionary<string, string>
        {
            { "Email", data.Email }
        });

        return blobClient.Uri.AbsolutePath;
    }
}