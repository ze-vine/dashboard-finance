using backend_csharp.Models;

namespace backend_csharp.Interfaces;

public interface IUsuarioRepositorio
{
    public Task<IEnumerable<Usuario>> ListarUsuariosAsync();
    public Task<int> AdicionarUsuarioAsync(Usuario usuario);
    public Task<Usuario> ObterUsuarioAsync(int id);
    public Task<bool> AtualizarUsuarioAsync(Usuario usuario);
    public Task<bool> ExcluirUsuarioAsync(int id);
}
