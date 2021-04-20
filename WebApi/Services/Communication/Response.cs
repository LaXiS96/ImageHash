using System;

namespace LaXiS.ImageHash.WebApi.Services.Communication
{
    public class Response
    {
        public bool Successful { get; }
        public string Message { get; }

        public Response(bool successful, string message = null)
        {
            Successful = successful;
            Message = message;
        }

        public Response(Exception e, string message = null)
        {
            Successful = false;
            Message = string.IsNullOrWhiteSpace(message) ? e.Message : string.Join(Environment.NewLine, new string[] { message, e.Message });
        }

        public static Response Success(string message = null) =>
            new Response(true, message);

        public static Response Failure(string message = null) =>
            new Response(false, message);

        public static Response Failure(Exception e, string message = null) =>
            new Response(e, message);
    }

    public class Response<T> : Response
    {
        public T Value { get; }

        public Response(bool success, string message = null)
            : base(success, message)
        { }

        public Response(Exception e, string message = null)
            : base(e, message)
        { }

        public Response(bool success, T value, string message = null)
            : base(success, message)
        {
            Value = value;
        }

        public static Response<T> Success(T value, string message = null) =>
            new Response<T>(true, value, message);

        public new static Response<T> Failure(string message = null) =>
            new Response<T>(false, message);

        public new static Response<T> Failure(Exception e, string message = null) =>
            new Response<T>(e, message);
    }
}
