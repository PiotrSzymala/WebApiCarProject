using FluentValidation;
using WebApiCar.Application.Constants;

namespace WebApiCar.Infrastructure.Validators
{
    public class UsernameValidator : AbstractValidator<string>
    {
        public UsernameValidator()
        {
            RuleFor(username => username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username cannot be empty")
                .MinimumLength(Settings.MinUsernameLength).WithMessage($"Username must be at least {Settings.MinUsernameLength} characters long")
                .MaximumLength(Settings.MaxUsernameLength).WithMessage($"Username cannot be longer than {Settings.MaxUsernameLength} characters")
                .Matches("^[a-zA-Z0-9_]*$").WithMessage("Username can only contain letters, numbers, and underscores");
        }
    }
}
