namespace WebApiCarProject.Infrastructure.ConnectionStrings;

public class PostgresSqlConnectionString
{
    public PostgresSqlConnectionString(IConfiguration config)
    {
        Value = config.GetConnectionString("PostgresSqlConnectionString");
    }

    public string Value { get; }
}