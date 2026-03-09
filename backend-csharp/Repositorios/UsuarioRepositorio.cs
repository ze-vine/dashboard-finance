using backend_csharp.Interfaces;
using backend_csharp.Models;
using Dapper;

namespace backend_csharp.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private DbSession _dbSession;
    private readonly string _nomeDaTabela = "usuarios";

    public UsuarioRepositorio(DbSession dbSession)
    {
        _dbSession = dbSession;
    }

    public async Task<IEnumerable<Usuario>> ListarUsuariosAsync()
    {
        string sql = $"SELECT * FROM {_nomeDaTabela} WHERE ativo = TRUE";
            return await _dbSession.Connection.QueryAsync<Usuario>(sql);
    }

    public async Task<Usuario> ObterUsuarioAsync(int id)
    {
        string sql = $"SELECT * FROM {_nomeDaTabela} WHERE id = @Id AND ativo = TRUE";
            return await _dbSession.Connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
    }

    public async Task<int> AdicionarUsuarioAsync(Usuario usuario)
    {
        string sql = $@"INSERT INTO {_nomeDaTabela} 
        (nome, sobrenome, cpf, email, senha, logradouro, numero_do_logradouro, cidade, estado, cep, 
        telefone_principal, telefone_secundario, ativo) 
        VALUES (@Nome, @Sobrenome, @Cpf, @Email, @Senha, @Logradouro, @NumeroDoLogradouro, @Cidade,
        @Estado, @Cep, @TelefonePrincipal, @TelefoneSecundario, TRUE);
        SELECT LAST_INSERT_ID();";
        
            return await _dbSession.Connection.ExecuteScalarAsync<int>(sql, usuario);
    }

    public async Task<bool> AtualizarUsuarioAsync(Usuario usuario)
    {
        string sql = $@"UPDATE {_nomeDaTabela} SET 
        nome = @Nome,
        sobrenome = @Sobrenome,
        cpf = @Cpf,
        email = @Email,
        logradouro = @Logradouro,
        numero_do_logradouro = @NumeroDoLogradouro,
        cidade = @Cidade,
        estado = @Estado,
        cep = @Cep,
        telefone_principal = @TelefonePrincipal,
        telefone_secundario = @TelefoneSecundario
        WHERE id = @Id AND ativo = TRUE";

            var linhasAfetadas = await _dbSession.Connection.ExecuteAsync(sql, usuario);
            return linhasAfetadas > 0;
    }

    public async Task<bool> ExcluirUsuarioAsync(int id)
    {
        string sql = $"UPDATE {_nomeDaTabela} SET ativo = FALSE WHERE id = @id";
        
            var linhasAfetadas = await _dbSession.Connection.ExecuteAsync(sql, new { id });
            return linhasAfetadas > 0;
    }

}