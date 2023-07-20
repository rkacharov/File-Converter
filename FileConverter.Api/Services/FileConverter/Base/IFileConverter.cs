using FileConverter.Api.Models.Dto;

namespace FileConverter.Api.Services.FileConverter.Base
{
    public interface IFileConverter
    {
        Task<ServiceResult<FileSaveDto>> Convert(FileConvertDto dto);
    }
}