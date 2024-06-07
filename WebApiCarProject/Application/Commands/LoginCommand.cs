using FluentValidation;
using MediatR;
using WebApiCarProject.Infrastructure.Validators;
using WebApiCarProject.Models;

namespace WebApiCarProject.Application.Commands;

public record LoginCommand : IRequest<bool>
{
    public LoginCommand(LoginForm input)
    {
        Username = input.Login;
        Password = input.Passwd;
    }

    public string Username { get; init; }
    public string Password { get; init; }
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