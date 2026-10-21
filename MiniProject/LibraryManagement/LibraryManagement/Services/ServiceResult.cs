namespace LibraryBooksSystem.Services
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public static ServiceResult Ok(string message)
        {
            ServiceResult result = new ServiceResult();
            result.Success = true;
            result.Message = message;
            return result;
        }

        public static ServiceResult Fail(string message)
        {
            ServiceResult result = new ServiceResult();
            result.Success = false;
            result.Message = message;
            return result;
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }

        public static ServiceResult<T> Ok(T data, string message)
        {
            ServiceResult<T> result = new ServiceResult<T>();
            result.Success = true;
            result.Message = message;
            result.Data = data;
            return result;
        }

        public new static ServiceResult<T> Fail(string message)
        {
            ServiceResult<T> result = new ServiceResult<T>();
            result.Success = false;
            result.Message = message;
            return result;
        }
    }
}