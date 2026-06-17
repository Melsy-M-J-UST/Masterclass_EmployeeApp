using System.Net;

namespace EmployeeApp.API.Dto
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }//HttpStatusCode
        public string Message { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
        public string Path { get; set; }
    }
}
