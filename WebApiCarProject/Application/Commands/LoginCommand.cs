using FluentValidation;
using MediatR;
using WebApiCar.Infrastructure.Validators;
using WebApiCar.Models;

namespace WebApiCar.Application.Commands
{
    public record LoginCommand : IRequest<bool>
    {
        public string Username { get; init; }
        public string Password { get; init; }
        public LoginCommand(LoginForm input)
        {
            Username = input.Username;
            Password = input.Password;
        }
    }

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(command => command.Username)
                .SetValidator(new UsernameValidator());

            RuleFor(command => command.Password)
                .SetValidator(new PasswordValidator());
        }
    }
}
