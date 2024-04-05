namespace WebApiCar.Infrastructure.ConnectionStrings
{
    public class PostgresSqlConnectionString
    {
        public string Value { get; }

        public PostgresSqlConnectionString(IConfiguration config)
        {
            Value = config.GetConnectionString("PostgresSqlConnectionString");
        }
    }
}
