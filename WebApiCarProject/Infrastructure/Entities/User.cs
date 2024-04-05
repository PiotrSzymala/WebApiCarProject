namespace WebApiCar.Infrastructure.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; init; }
        public string PasswordHash { get; init; }
    }
}
