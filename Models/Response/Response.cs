using System.Net;

namespace Models.Response
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Msg { get; set; }
        public object? Data { get; set; }

        public static Response FillObject(object data, HttpStatusCode statusCode, string msg)
        {
            return new Response()
            {
                Data = data,
                StatusCode = statusCode,
                Msg = msg
            };
        }
    }
}
