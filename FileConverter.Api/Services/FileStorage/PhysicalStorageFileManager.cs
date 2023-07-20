using FileConverter.Api.Models.Dto;
using FileConverter.Api.Options;
using FileConverter.Api.Services.FileStorage.Base;
using Microsoft.Extensions.Options;

namespace FileConverter.Api.Services.FileStorage
{
    public class PhysicalStorageFileManager : IFileManager
    {
        private readonly ILogger<PhysicalStorageFileManager> _logger;
        private readonly PhysicalStorageOptions _storageOptions;

        public PhysicalStorageFileManager(
            ILogger<PhysicalStorageFileManager> logger,
            IOptions<PhysicalStorageOptions> storageOptions)
        {
            _logger = logger;
            _storageOptions = storageOptions.Value;
        }

        public async Task<ServiceResult> SaveFile(FileSaveDto dto)
        {
            try
            {
                var fileContent = dto.FileContent;
                var fileName = dto.FileName;
                var directory = _storageOptions.ConvertedFileDirectory;
                var fullPath = Path.Combine(directory, fileName);

                if(!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                await using var sourceStream = new FileStream(
                    fullPath,
                    FileMode.CreateNew,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 4096,
                    useAsync: true);

                await sourceStream.WriteAsync(fileContent, 0, fileContent.Length);

                return ServiceResult.Succeed();
            }
            catch (IOException ioException)
            {
                _logger.LogError(ioException, ioException.Message);
                return ServiceResult.Fail(ioException.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.Fail("The file save failed unexpectedly. Contact support for more details.");
            }
        }
    }
}