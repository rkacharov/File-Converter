using System.Xml;

namespace FileConverter.Api.Models.Dto
{
    public class FileConvertDto
    {
        public XmlDocument XmlDocument { get; set; } = new();
        public string FileName { get; set; } = string.Empty;
    }
}