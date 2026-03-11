using backend_csharp.Models;

namespace backend_csharp.Interfaces;

public interface ICategoriaRepositorio
{
  public Task<IEnumerable<Categoria>> ListarCategoriasDoUsuario(int idUsuario);
}