using System.Text;
using FileConverter.Api.Models.Dto;
using FileConverter.Api.Options;
using FileConverter.Api.Services.FileStorage;
using FileConverter.Api.Services.FileStorage.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace FileConverter.Api.UnitTests.Services.FileStorage
{
    internal class PhysicalStorageFileManagerTests : BaseTestFixture
    {
        private const string ValidJsonString = "{\"root\":{\"prop\":123}}";
        private IFileManager _manager;
        private string _tempDirectory;

        [SetUp]
        public void Setup()
        {
            _tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            var storageOptions = new PhysicalStorageOptions()
            {
                ConvertedFileDirectory = _tempDirectory
            };

            _manager = new PhysicalStorageFileManager(
                LoggerFactory.CreateLogger<PhysicalStorageFileManager>(),
                new OptionsWrapper<PhysicalStorageOptions>(storageOptions));
        }

        [Test]
        public async Task FileSaveWithDuplicateNames_OnSaveFile_ShouldFail()
        {
            var saveResult = await _manager.SaveFile(new FileSaveDto()
            {
                FileContent = Encoding.UTF8.GetBytes(ValidJsonString),
                FileName = "duplicate.json"
            });

            Assert.That(saveResult.Succeeded, Is.True);

            saveResult = await _manager.SaveFile(new FileSaveDto()
            {
                FileContent = Encoding.UTF8.GetBytes(ValidJsonString),
                FileName = "duplicate.json"
            });

            Assert.That(saveResult.Succeeded, Is.False);
        }

        [Test]
        public async Task FileSave_OnSaveFile_ShouldSucceed()
        {
            var saveResult = await _manager.SaveFile(new FileSaveDto()
            {
                FileContent = Encoding.UTF8.GetBytes(ValidJsonString),
                FileName = "test.json"
            });

            Assert.That(saveResult.Succeeded, Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(_tempDirectory, true);
        }
    }
}