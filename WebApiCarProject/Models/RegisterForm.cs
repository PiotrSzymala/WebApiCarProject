namespace WebApiCar.Models
{
    public record RegisterForm
    {
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }

}
