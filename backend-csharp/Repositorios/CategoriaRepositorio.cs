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
  /* Ao concluir o CRUD das categorias, adicionarei a validação do usuário logado, obtendo o id do usuário
  a partir do JWT. */
  public async Task<IEnumerable<Categoria>> ListarCategoriasDoUsuario(int idUsuario)
  {
    string sql = @"SELECT id AS Id, nome AS Nome, ativo As Ativo 
    FROM categorias WHERE id_usuario = @IdUsuario AND ativo = TRUE";
    
    return await _dbSession.Connection.QueryAsync<Categoria>(sql, new { IdUsuario = idUsuario });
  }
  
}

