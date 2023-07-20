using FileConverter.Api.Services.FileConverter.Base;
using FileConverter.Api.Services.FileStorage.Base;
using FileConverter.Api.Services.FileValidator.Base;

namespace FileConverter.Api.Models.Dto
{
    public class FileServiceContainerDto
    {
        public IFileValidator? FileValidator { get; set; }
        public IFileConverter? FileConverter { get; set; }
        public IFileManager? FileManager { get; set; }
    }
}