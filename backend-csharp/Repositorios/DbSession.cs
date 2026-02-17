using System.Data;
using MySql.Data.MySqlClient;

namespace backend_csharp.Repositorios;

public class DbSession : IDisposable
{

    public IDbConnection _connection { get; }

    public DbSession(IConfiguration configuration)
    {
        _connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
        _connection.Open();
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}