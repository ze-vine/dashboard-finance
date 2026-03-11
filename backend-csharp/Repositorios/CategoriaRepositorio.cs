using backend_csharp.Models;
using backend_csharp.Interfaces;
using Dapper;

namespace backend_csharp.Repositorios;

public class CategoriaRepositorio : ICategoriaRepositorio
{
  private DbSession _dbSession;
  
  public CategoriaRepositorio(DbSession dbSession)
  {
    _dbSession = dbSession;
  }
  
  public async Task<IEnumerable<Categoria>> ListarCategoriasDoUsuario(int idUsuario)
  {
    var sql = "SELECT id, nome FROM categorias WHERE id_usuario = @idUsuario";
    return await _dbSession.Connection.QueryAsync<Categoria>(sql, new { idUsuario });
  }
  
}

