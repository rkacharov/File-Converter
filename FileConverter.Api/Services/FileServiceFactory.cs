using FileConverter.Api.Common;
using FileConverter.Api.Enums;
using FileConverter.Api.Models.Dto;
using FileConverter.Api.Services.FileConverter.Base;
using FileConverter.Api.Services.FileStorage.Base;
using FileConverter.Api.Services.FileValidator.Base;

namespace FileConverter.Api.Services
{
    public class FileServiceFactory : IFileServiceFactory
    {
        private readonly ILogger<FileServiceFactory> _logger;
        private readonly IFileValidatorFactory _fileValidatorFactory;
        private readonly IFileConverterFactory _fileConverterFactory;
        private readonly IFileManagerFactory _fileManagerFactory;

        public FileServiceFactory(
            ILogger<FileServiceFactory> logger,
            IFileValidatorFactory fileValidatorFactory,
            IFileConverterFactory converterFactory,
            IFileManagerFactory fileManagerFactory)
        {
            _logger = logger;
            _fileValidatorFactory = fileValidatorFactory;
            _fileConverterFactory = converterFactory;
            _fileManagerFactory = fileManagerFactory;
        }

        public Task<ServiceResult<FileServiceContainerDto>> CreateServices(
            SourceFileType fileType,
            FileConverterType converterType,
            FileManagerType managerType)
        {
            try
            {
                var fileServiceContainer = new FileServiceContainerDto()
                {
                    FileValidator = _fileValidatorFactory.Create(fileType),
                    FileConverter = _fileConverterFactory.Create(converterType),
                    FileManager = _fileManagerFactory.Create(managerType)
                };

                return Task.FromResult(ServiceResult<FileServiceContainerDto>.Succeed(fileServiceContainer));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Task.FromResult(ServiceResult<FileServiceContainerDto>.Fail(ExceptionMessages.InternalServerErrorMessage));
            }
        }
    }
}