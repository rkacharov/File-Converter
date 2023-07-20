using System.Xml;
using FileConverter.Api.Models;
using FileConverter.Api.Models.Dto;
using FileConverter.Api.Services.FileValidator.Base;

namespace FileConverter.Api.Services.FileValidator
{
    public class XmlFileValidator : IFileValidator
    {
        private readonly ILogger<XmlFileValidator> _logger;

        public XmlFileValidator(ILogger<XmlFileValidator> logger)
        {
            _logger = logger;
        }

        public async Task<ServiceResult<FileConvertDto>> Validate(FileUploadModel model)
        {
            var file = model.File;
            var fileName = Path.GetFileName(file?.FileName);

            if(file == null || file.Length == 0 || string.IsNullOrWhiteSpace(fileName))
            {
                return ServiceResult<FileConvertDto>.Fail("File and file name cannot be null or empty.");
            }

            try
            {
                if(Path.GetExtension(fileName).ToLowerInvariant() != ".xml")
                {
                    return ServiceResult<FileConvertDto>.Fail("The file extension is invalid.");
                }

                // This will fail with XmlException if the uploaded file content is not valid
                var xmlDocument = new XmlDocument();
                await using(var fileStream = file.OpenReadStream())
                {
                    xmlDocument.Load(fileStream);
                }

                var fileConvertDto = new FileConvertDto()
                {
                    XmlDocument = xmlDocument,
                    FileName = Path.GetFileNameWithoutExtension(fileName)
                };

                return ServiceResult<FileConvertDto>.Succeed(fileConvertDto);
            }
            catch(XmlException xmlEx)
            {
                _logger.LogError(xmlEx, xmlEx.Message);
                return ServiceResult<FileConvertDto>.Fail(xmlEx.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult<FileConvertDto>.Fail("The file validation failed unexpectedly. Contact support for more details.");
            }
        }
    }
}