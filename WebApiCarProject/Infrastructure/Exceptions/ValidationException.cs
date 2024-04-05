using System.Net;

namespace WebApiCar.Infrastructure.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public Dictionary<string, string[]> Errors { get; set; }
        public ValidationException(Dictionary<string, string[]> errors) : base((int)HttpStatusCode.Forbidden)
        {
            Errors = errors;
        }
    }
}
