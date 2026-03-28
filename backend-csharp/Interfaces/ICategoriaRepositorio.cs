using backend_csharp.DTO;
using backend_csharp.Models;

namespace backend_csharp.Interfaces;

public interface ICategoriaRepositorio
{
    public Task<IEnumerable<Categoria>> ListarCategoriasDoUsuario(int idUsuario);
    public Task<Categoria> CriarUmaCategoria(Categoria categoriaModel);
    public Task<Categoria> BuscarUmaCategoria(int idUsuario, int idDaCategoria);
}
