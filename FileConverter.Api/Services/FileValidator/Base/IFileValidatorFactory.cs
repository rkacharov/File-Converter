using FileConverter.Api.Enums;

namespace FileConverter.Api.Services.FileValidator.Base
{
    public interface IFileValidatorFactory
    {
        IFileValidator Create(SourceFileType sourceFileType);
    }
}