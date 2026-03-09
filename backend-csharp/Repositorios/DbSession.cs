using System.Data;
using MySqlConnector;

namespace backend_csharp.Repositorios;

public class DbSession : IDisposable
{
    public IDbConnection Connection { get; }

    public DbSession(IConfiguration configuration)
    {
        Connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public void Dispose()
    {
        if (Connection != null && Connection.State != ConnectionState.Closed)
        {
            Connection.Dispose();
        }
    }
}
