namespace WebApiCarProject.Infrastructure.Exceptions;

public class ApplicationException : Exception
{
    public ApplicationException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public ApplicationException(int statusCode)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; set; }

    public override string ToString()
    {
        return $"{{ StatusCode: {StatusCode}, Message: {Message} }}";
    }
}