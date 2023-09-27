using MarketPlace.Core.Interfaces.DapperInterfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MarketPlace.Core.Services;
public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public SqlConnection CreateConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("ApplicationConnection"));
    }
}
