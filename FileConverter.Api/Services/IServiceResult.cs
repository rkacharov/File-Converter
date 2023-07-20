namespace FileConverter.Api.Services
{
    public interface IServiceResult
    {
        bool Succeeded { get; }
        string? ErrorMessage { get; }
    }
}