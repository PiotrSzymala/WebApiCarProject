using FluentValidation;
using MediatR;
using WebApiCarProject.Infrastructure.Validators;
using WebApiCarProject.Models;

namespace WebApiCarProject.Application.Commands;

public record RegisterCommand : IRequest<bool>
{
    public RegisterCommand(RegisterForm input)
    {
        Username = input.Login;
        Password = input.Paswd;
    }

    public string Username { get; init; }
    public string Password { get; init; }
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