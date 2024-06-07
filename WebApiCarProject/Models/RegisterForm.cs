namespace WebApiCarProject.Models;

public record RegisterForm
{
    public string Login { get; init; } = string.Empty;
    public string Passwd { get; init; } = string.Empty;
}