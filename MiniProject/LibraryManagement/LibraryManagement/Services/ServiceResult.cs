namespace LibraryManagement.Services;

internal class ServiceResult
{
    public bool Success { get; }
    public string Message { get; }
    protected ServiceResult(bool success, string message) { Success = success; Message = message; }
    public static ServiceResult Ok(string message) => new(true, message);
    public static ServiceResult Fail(string message) => new(false, message);
}

internal sealed class ServiceResult<T> : ServiceResult
{
    public T? Data { get; }
    private ServiceResult(bool success, string message, T? data) : base(success, message) { Data = data; }
    public static ServiceResult<T> Ok(T data, string message) => new(true, message, data);
    public new static ServiceResult<T> Fail(string message) => new(false, message, default);
}
