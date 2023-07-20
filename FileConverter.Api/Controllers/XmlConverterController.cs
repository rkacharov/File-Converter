using FileConverter.Api.Common;
using FileConverter.Api.Enums;
using FileConverter.Api.Models;
using FileConverter.Api.Models.Dto;
using FileConverter.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileConverter.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class XmlConverterController : ControllerBase
    {
        private readonly ILogger<XmlConverterController> _logger;
        private readonly IFileServiceFactory _fileServiceFactory;
        public XmlConverterController(
            ILogger<XmlConverterController> logger,
            IFileServiceFactory fileServiceFactory)
        {
            _logger = logger;
            _fileServiceFactory = fileServiceFactory;
        }

        [HttpPost]
        public async Task<IActionResult> ToJson([FromForm] FileUploadModel fileUpload)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResult<FileServiceContainerDto> fileServicesResult =
                await _fileServiceFactory.CreateServices(SourceFileType.Xml, FileConverterType.XmlToJson, FileManagerType.PhysicalStorage);
            if(!fileServicesResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, fileServicesResult.ErrorMessage);
            }

            FileServiceContainerDto fileServices = fileServicesResult.Content;

            ServiceResult<FileConvertDto> validationResult = await fileServices.FileValidator!.Validate(fileUpload);
            if(!validationResult.Succeeded)
            {
                return BadRequest(validationResult.ErrorMessage);
            }

            ServiceResult<FileSaveDto> conversionResult = await fileServices.FileConverter!.Convert(validationResult.Content);
            if(!conversionResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ExceptionMessages.InternalServerErrorMessage);
            }

            ServiceResult saveResult = await fileServices.FileManager!.SaveFile(conversionResult.Content);
            if(!saveResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, saveResult.ErrorMessage);
            }

            return Ok();
        }
    }
}