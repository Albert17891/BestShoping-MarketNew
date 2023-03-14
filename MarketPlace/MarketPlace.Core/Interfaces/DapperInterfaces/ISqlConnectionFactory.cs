using Microsoft.Data.SqlClient;

namespace MarketPlace.Core.Interfaces.DapperInterfaces;
public interface ISqlConnectionFactory
{	
    SqlConnection CreateConnection();
}
