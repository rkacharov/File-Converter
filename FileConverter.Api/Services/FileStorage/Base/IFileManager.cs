using FileConverter.Api.Models.Dto;

namespace FileConverter.Api.Services.FileStorage.Base
{
    public interface IFileManager
    {
        Task<ServiceResult> SaveFile(FileSaveDto fileSaveDto);
    }
}