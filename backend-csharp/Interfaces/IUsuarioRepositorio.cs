using backend_csharp.Models;

namespace backend_csharp.Interfaces;

public interface IUsuarioRepositorio
{
    Task<IEnumerable<Usuario>> ListarTodos();
}

