namespace WebApiCar.Models
{
    public record LoginForm
    {
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
