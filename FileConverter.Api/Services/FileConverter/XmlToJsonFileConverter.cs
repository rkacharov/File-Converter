using System.Text;
using FileConverter.Api.Models.Dto;
using FileConverter.Api.Services.FileConverter.Base;
using Newtonsoft.Json;

namespace FileConverter.Api.Services.FileConverter
{
    public class XmlToJsonFileConverter : IFileConverter
    {
        private readonly ILogger<XmlToJsonFileConverter> _logger;

        public XmlToJsonFileConverter(ILogger<XmlToJsonFileConverter> logger)
        {
            _logger = logger;
        }

        public Task<ServiceResult<FileSaveDto>> Convert(FileConvertDto dto)
        {
            try
            {
                var jsonString = JsonConvert.SerializeXmlNode(dto.XmlDocument);
                var jsonByteArr = Encoding.UTF8.GetBytes(jsonString);

                var fileSaveDto = new FileSaveDto()
                {
                    FileContent = jsonByteArr,
                    FileName = dto.FileName + ".json"
                };

                return Task.FromResult(ServiceResult<FileSaveDto>.Succeed(fileSaveDto));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Task.FromResult(ServiceResult<FileSaveDto>.Fail("The file conversion failed unexpectedly. Contact support for more details."));
            }
        }
    }
}