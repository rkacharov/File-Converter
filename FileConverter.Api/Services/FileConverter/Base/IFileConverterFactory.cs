using FileConverter.Api.Enums;

namespace FileConverter.Api.Services.FileConverter.Base
{
    public interface IFileConverterFactory
    {
        IFileConverter Create(FileConverterType converterType);
    }
}