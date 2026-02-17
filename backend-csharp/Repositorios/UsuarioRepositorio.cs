using backend_csharp.Interfaces;
using backend_csharp.Models;
using Dapper;
using MySqlConnector;

namespace backend_csharp.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private DbSession _dbSession;

    public UsuarioRepositorio(DbSession dbSession)
    {
        _dbSession = dbSession;
    }

    public async Task<IEnumerable<Usuario>> ListarUsuarios()
    {
        string sql = "SELECT * FROM usuarios";
        using (var connection = _dbSession._connection)
        {
            return await connection.QueryAsync<Usuario>(sql: sql);
        }
    }
}