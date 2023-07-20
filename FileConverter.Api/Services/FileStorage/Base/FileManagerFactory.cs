using FileConverter.Api.Enums;

namespace FileConverter.Api.Services.FileStorage.Base
{
    public class FileManagerFactory : IFileManagerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public FileManagerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IFileManager Create(FileManagerType fileManagerType)
        {
            switch(fileManagerType)
            {
                case FileManagerType.PhysicalStorage:
                    return _serviceProvider.GetService<PhysicalStorageFileManager>() ??
                           throw new InvalidOperationException($"{nameof(PhysicalStorageFileManager)} is not registered in the DI container.");
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileManagerType), fileManagerType, "File manager type is not supported.");
            }
        }
    }
}