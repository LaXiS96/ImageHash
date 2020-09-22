namespace LaXiS.ImageHash.WebApi.Services.Communication
{
    public class Response
    {
        public bool Success { get; }
        public string Message { get; }

        public Response(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

    public class Response<T> : Response
    {
        public T Value { get; }

        public Response(bool success, string message, T value) : base(success, message)
        {
            Value = value;
        }
    }
}
