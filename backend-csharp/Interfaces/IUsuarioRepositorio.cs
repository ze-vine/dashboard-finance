using backend_csharp.Models;

namespace backend_csharp.Interfaces;

public interface IUsuarioRepositorio
{
    public Task<IEnumerable<Usuario>> ListarUsuariosAsync();
}

