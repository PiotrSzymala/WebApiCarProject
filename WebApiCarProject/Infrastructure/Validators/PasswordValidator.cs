using FluentValidation;
using WebApiCar.Application.Constants;

namespace WebApiCar.Infrastructure.Validators
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(password => password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(Settings.MinPasswordLength).WithMessage($"Password must be at least {Settings.MinPasswordLength} characters long")
                .MaximumLength(Settings.MaxPasswordLength).WithMessage($"Password cannot be longer than {Settings.MaxPasswordLength} characters");
        }
    }
}
