using System.Xml;
using Microsoft.Extensions.Logging;
using FileConverter.Api.Services.FileConverter;
using FileConverter.Api.Services.FileConverter.Base;
using FileConverter.Api.Models.Dto;

namespace FileConverter.Api.UnitTests.Services.FileConverter
{
    internal class XmlToJsonFileConverterTests : BaseTestFixture
    {
        private IFileConverter _converter;

        [SetUp]
        public void Setup()
        {
            _converter = new XmlToJsonFileConverter(LoggerFactory.CreateLogger<XmlToJsonFileConverter>());
        }

        [Test]
        public async Task FileConvert_OnConvert_ShouldSucceed()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<Root><Prop>123</Prop></Root>");

            var validationResult = await _converter.Convert(new FileConvertDto()
            {
                XmlDocument = xmlDocument,
                FileName = "test.xml"
            });

            Assert.That(validationResult.Succeeded, Is.True);
        }
    }
}
