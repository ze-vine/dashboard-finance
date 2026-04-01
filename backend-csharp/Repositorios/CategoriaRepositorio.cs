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

  public async Task<Categoria> BuscarUmaCategoria(int idUsuario, int idDaCategoria)
  {
    string sql = @"SELECT id AS Id, nome AS Nome, ativo AS Ativo FROM categorias
    WHERE id = @Id AND id_usuario = @IdUsuario";

    return await _dbSession.Connection.QueryFirstOrDefaultAsync<Categoria>(sql, new
    {
      Id = idDaCategoria,
      IdUsuario = idUsuario
    });
  }

  public async Task<Categoria> CriarUmaCategoria(Categoria categoriaModel)
  {
    string sql = @"INSERT INTO categorias(nome, id_usuario) VALUES (@Nome, @IdUsuario);
    SELECT LAST_INSERT_ID();";
  
    int idGerado = await _dbSession.Connection.ExecuteScalarAsync<int>(sql, categoriaModel);

    categoriaModel.Id = idGerado;

    return categoriaModel;
  }

  public async Task<bool> AtualizarUmaCategoria(Categoria categoriaModel)
  {
    string sql = @"UPDATE categorias SET nome = @Nome 
    WHERE id = @Id AND id_usuario = @IdUsuario";

    return await _dbSession.Connection.ExecuteAsync(sql, categoriaModel) > 0;
  }

  public async Task<bool> DesativarUmaCategoria(int idDaCategoria, int idUsuario)
  {
    string sql = "UPDATE categorias SET ativo = FALSE WHERE id = @Id AND id_usuario = @IdUsuario";

    return await _dbSession.Connection.ExecuteAsync(sql, new { Id = idDaCategoria, IdUsuario = idUsuario }) > 0;
  }
  
}

