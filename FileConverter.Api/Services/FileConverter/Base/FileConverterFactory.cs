using FileConverter.Api.Enums;

namespace FileConverter.Api.Services.FileConverter.Base
{
    public class FileConverterFactory : IFileConverterFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public FileConverterFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IFileConverter Create(FileConverterType converterType)
        {
            switch(converterType)
            {
                case FileConverterType.XmlToJson:
                    return _serviceProvider.GetService<XmlToJsonFileConverter>() ??
                           throw new InvalidOperationException($"{nameof(XmlToJsonFileConverter)} is not registered in the DI container.");
                default:
                    throw new ArgumentOutOfRangeException(nameof(converterType), converterType, "Converter type not supported.");
            }
        }
    }
}