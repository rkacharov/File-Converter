using FileConverter.Api.Enums;
using FileConverter.Api.Models.Dto;

namespace FileConverter.Api.Services
{
    public interface IFileServiceFactory
    {
        Task<ServiceResult<FileServiceContainerDto>> CreateServices(
            SourceFileType fileType,
            FileConverterType converterType,
            FileManagerType managerType);
    }
}
