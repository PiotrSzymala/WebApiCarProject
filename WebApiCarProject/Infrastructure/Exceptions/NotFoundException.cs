using Microsoft.AspNetCore.Http;

namespace WebApiCarProject.Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public int StatusCode { get; set; }
        public NotFoundException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public NotFoundException(int statusCode)
        {
            StatusCode = statusCode;
        }
        public override string ToString()
        {
            return $"{{ StatusCode: {StatusCode}, Message: {Message} }}";
        }
    }
}
