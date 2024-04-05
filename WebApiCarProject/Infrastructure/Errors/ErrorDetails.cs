using System.Text.Json;

namespace WebApiCar.Infrastructure.Errors
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
