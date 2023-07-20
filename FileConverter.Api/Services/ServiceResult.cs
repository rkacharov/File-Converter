namespace FileConverter.Api.Services
{
    public class ServiceResult : IServiceResult
    {
        public static ServiceResult Succeed()
        {
            return new ServiceResult(true, null);
        }

        public static ServiceResult Fail(string? message = null)
        {
            return new ServiceResult(false, message);
        }

        protected ServiceResult(bool succeeded, string? errorMessage)
        {
            Succeeded = succeeded;
            ErrorMessage = errorMessage;
        }

        public bool Succeeded { get; }
        public string? ErrorMessage { get; }
    }

    public class ServiceResult<T> : IServiceResult
    {
        public T Content { get; }
        public bool Succeeded { get; }
        public string? ErrorMessage { get; }

        public static ServiceResult<T> Succeed(T result)
        {
            return new ServiceResult<T>(true, null, result);
        }

        public static ServiceResult<T> Fail(string? message = null)
        {
            return new ServiceResult<T>(false, message, default(T));
        }

        protected ServiceResult(bool succeeded, string? errorMessage, T content)
        {
            Succeeded = succeeded;
            ErrorMessage = errorMessage;
            Content = content;
        }
    }
}