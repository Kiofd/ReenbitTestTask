using Test_Task_.Net_Reenbit_Masksym_Sheremeta.Models;

namespace Test_Task_.Net_Reenbit_Masksym_Sheremeta.Services;

public interface IFileService
{
    Task<string> UploadFileAsync(Blob data);
}