using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WebApiCar.Infrastructure.Validators;
using WebApiCar.Models;

namespace WebApiCar.Application.Commands
{
    public record RegisterCommand : IRequest<bool>
    {
        public string Username { get; init; }
        public string Password { get; init; }
        public RegisterCommand(RegisterForm input)
        {
            Username = input.Username;
            Password = input.Password;
        }
    }

    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(command => command.Username)
                .SetValidator(new UsernameValidator());

            RuleFor(command => command.Password)
                .SetValidator(new PasswordValidator());
        }
    }
}