using FileConverter.Api.Enums;

namespace FileConverter.Api.Services.FileValidator.Base
{
    public class FileValidatorFactory : IFileValidatorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public FileValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IFileValidator Create(SourceFileType sourceFileType)
        {
            switch(sourceFileType)
            {
                case SourceFileType.Xml:
                    return _serviceProvider.GetService<XmlFileValidator>() ??
                           throw new InvalidOperationException($"{nameof(XmlFileValidator)} is not registered in the DI container.");
                default:
                    throw new ArgumentOutOfRangeException(nameof(sourceFileType), sourceFileType, "Provided file type is no supported.");
            }
        }
    }
}