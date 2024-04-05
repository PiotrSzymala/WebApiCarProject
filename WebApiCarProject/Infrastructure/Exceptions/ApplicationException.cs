namespace WebApiCar.Infrastructure.Exceptions
{
    public class ApplicationException : Exception
    {
        public int StatusCode { get; set; }

        public ApplicationException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApplicationException(int statusCode) : base()
        {
            StatusCode = statusCode;
        }

        public override string ToString()
        {
            return $"{{ StatusCode: {StatusCode}, Message: {Message} }}";
        }
    }
}
