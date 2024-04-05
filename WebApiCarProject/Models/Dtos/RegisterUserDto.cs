namespace WebApiCar.Models.Dtos
{
    public class RegisterUserDto
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
