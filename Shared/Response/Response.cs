namespace Shared.Response
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Value { get; set; } 
        public static Response<T> Success(T value ) => new Response<T> { IsSuccess = true, Value = value };
        public static Response<T> Fail(string message) => new Response<T> { IsSuccess = false, Message = message };
    }
}
