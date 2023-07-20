using System.ComponentModel.DataAnnotations;

namespace FileConverter.Api.Models
{
    public class FileUploadModel
    {
        [Required]
        public IFormFile? File { get; set; }
    }
}