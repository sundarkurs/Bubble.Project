
namespace Data.ClientModels.Comm
{
    public class Response<T> where T : new()
    {
        public Response()
        {
            IsSuccess = true;
            Message = string.Empty;
        }

        public Response(dynamic data) : this()
        {
            Data = data;
        }

        public Response(bool success, string message)
        {
            IsSuccess = success;
            Message = message;
        }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public dynamic Data { get; set; }
    }
}
