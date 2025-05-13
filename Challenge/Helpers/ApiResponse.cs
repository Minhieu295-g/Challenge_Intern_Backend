using System.Net;

namespace MyAppDemo.Helpers
{
    public class ApiResponse<T>
    {
        public HttpStatusCode Status { get; set; }
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
