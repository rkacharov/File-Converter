using FileConverter.Api.Models;
using FileConverter.Api.Models.Dto;

namespace FileConverter.Api.Services.FileValidator.Base
{
    public interface IFileValidator
    {
        Task<ServiceResult<FileConvertDto>> Validate(FileUploadModel model);
    }
}