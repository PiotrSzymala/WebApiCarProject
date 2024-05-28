namespace WebApiCarProject.Models;

public record RegisterForm
{
    public string Login { get; init; } = string.Empty;
    public string Paswd { get; init; } = string.Empty;
}