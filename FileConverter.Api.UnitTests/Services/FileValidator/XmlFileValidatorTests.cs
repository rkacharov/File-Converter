using System.Text;
using FileConverter.Api.Models;
using FileConverter.Api.Services.FileValidator;
using FileConverter.Api.Services.FileValidator.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FileConverter.Api.UnitTests.Services.FileValidator
{
    internal class XmlFileValidatorTests : BaseTestFixture
    {
        private const string ValidXmlString = "<Root><Prop>123</Prop></Root>";
        private const string InvalidXmlString = "this is not an XML";
        private const string ValidFileName = "test.xml";

        private IFileValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new XmlFileValidator(LoggerFactory.CreateLogger<XmlFileValidator>());
        }

        [Test]
        public async Task FileUploadWithNoFile_OnValidate_ShouldFail()
        {
            var validationResult = await _validator.Validate(new FileUploadModel()
            {
                File = null
            });

            Assert.That(validationResult.Succeeded, Is.False);
        }

        [Test]
        public async Task FileUploadWithEmptyFile_OnValidate_ShouldFail()
        {
            var validationResult = await _validator.Validate(new FileUploadModel()
            {
                File = _CreateTestFormFile(ValidFileName, string.Empty)
            });

            Assert.That(validationResult.Succeeded, Is.False);
        }

        [Test]
        public async Task FileUploadWithNoFileName_OnValidate_ShouldFail()
        {
            var validationResult = await _validator.Validate(new FileUploadModel()
            {
                File = _CreateTestFormFile(null, ValidXmlString)
            });

            Assert.That(validationResult.Succeeded, Is.False);
        }

        [Test]
        public async Task FileUploadWithEmptyFileName_OnValidate_ShouldFail()
        {
            var validationResult = await _validator.Validate(new FileUploadModel()
            {
                File = _CreateTestFormFile(string.Empty, ValidXmlString)
            });

            Assert.That(validationResult.Succeeded, Is.False);
        }

        [Test]
        public async Task FileUploadWithInvalidExtension_OnValidate_ShouldFail()
        {
            var validationResult = await _validator.Validate(new FileUploadModel()
            {
                File = _CreateTestFormFile("test.pdf", ValidXmlString)
            });

            Assert.That(validationResult.Succeeded, Is.False);
        }

        [Test]
        public async Task FileUploadWithInvalidContent_OnValidate_ShouldFail()
        {
            var validationResult = await _validator.Validate(new FileUploadModel()
            {
                File = _CreateTestFormFile(ValidFileName, InvalidXmlString)
            });

            Assert.That(validationResult.Succeeded, Is.False);
        }

        [Test]
        public async Task FileUpload_OnValidate_ShouldSucceed()
        {
            var validationResult = await _validator.Validate(new FileUploadModel()
            {
                File = _CreateTestFormFile(ValidFileName, ValidXmlString)
            });

            Assert.That(validationResult.Succeeded, Is.True);
        }

        private IFormFile _CreateTestFormFile(string? fileName, string content)
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            var memoryStream = new MemoryStream(bytes);

            return new FormFile(
                baseStream: memoryStream,
                baseStreamOffset: 0,
                length: bytes.Length,
                name: "Data",
                fileName: fileName
            );
        }
    }
}