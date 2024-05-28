using System.Net;

namespace WebApiCarProject.Infrastructure.Exceptions;

public class ValidationException : ApplicationException
{
    public ValidationException(Dictionary<string, string[]> errors) : base((int)HttpStatusCode.Forbidden)
    {
        Errors = errors;
    }

    public Dictionary<string, string[]> Errors { get; set; }
}