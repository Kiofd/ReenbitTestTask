using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace Test_Task_.Net_Reenbit_Masksym_Sheremeta.Services;

public class SasToken
{
    private readonly string _connectionString;
    private readonly string _conteiner;

    public SasToken( string connectionString, string conteiner)
    {
        _connectionString = connectionString;
        _conteiner = conteiner;
    }

    public string GenerateSasToken()
    {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);

        CloudBlobClient client = storageAccount.CreateCloudBlobClient();

        CloudBlobContainer container = client.GetContainerReference(_conteiner);

        SharedAccessBlobPolicy accessPolicy = new SharedAccessBlobPolicy
        {
            // Define expiration to be 60 minutes from now in UTC
            SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(60),

            // Add permissions
            Permissions = SharedAccessBlobPermissions.Create | SharedAccessBlobPermissions.Write
        };

        string token = container.GetSharedAccessSignature(accessPolicy);

        string containerUri = container.Uri.ToString();

        string sasUriToken = $"{containerUri}{token}";

        return sasUriToken;
    }
}