namespace FileConverter.Api.Models.Dto
{
    public class FileSaveDto
    {
        public byte[] FileContent { get; set; } = Array.Empty<byte>();
        public string FileName { get; set; } = string.Empty;
    }
}