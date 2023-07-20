using FileConverter.Api.Enums;

namespace FileConverter.Api.Services.FileStorage.Base
{
    public interface IFileManagerFactory
    {
        IFileManager Create(FileManagerType fileManagerType);
    }
}