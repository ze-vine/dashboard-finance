using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using backend_csharp.Interfaces;
using backend_csharp.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

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
        string sql = $"SELECT * FROM {_nomeDaTabela}";
        using (var connection = _dbSession._connection)
        {
            return await connection.QueryAsync<Usuario>(sql);
        }
    }

    public async Task<Usuario> ObterUsuarioAsync(int id)
    {
        string sql = $"SELECT * FROM {_nomeDaTabela} WHERE id = @Id";
        using (var connection = _dbSession._connection)
        {
            return await connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
        }
    }

    public async Task<int> AdicionarUsuarioAsync(Usuario usuario)
    {
        string sql = $@"INSERT INTO {_nomeDaTabela} 
        (nome, sobrenome, email, senha, logradouro, numero_do_logradouro, cidade, estado, cep, 
        telefone_principal, telefone_secundario) 
        VALUES (@Nome, @Sobrenome, @Email, @Senha, @Logradouro, @NumeroDoLogradouro, @Cidade,
        @Estado, @Cep, @TelefonePrincipal, @TelefoneSecundario);
        SELECT LAST_INSERT_ID();";

        using (var conneciton = _dbSession._connection)
        {
            return await conneciton.ExecuteScalarAsync<int>(sql, usuario);
        }
    }

    public async Task<bool> AtualizarUsuarioAsync(Usuario usuario)
    {
        string sql = $@"UPDATE {_nomeDaTabela} SET 
        nome = @Nome,
        sobrenome = @Sobrenome,
        email = @Email,
        logradouro = @Logradouro,
        numero_do_logradouro = @NumeroDoLogradouro,
        cidade = @Cidade,
        estado = @Estado,
        cep = @Cep,
        telefone_principal = @TelefonePrincipal,
        telefone_secundario = @TelefoneSecundario
        WHERE id = @Id";

        using (var connection = _dbSession._connection)
        {
            var linhasAfetadas = await connection.ExecuteAsync(sql, usuario);
            return linhasAfetadas > 0;
        }
    }

    public async Task<bool> ExcluirUsuarioAsync(int id)
    {
        string sql = $"DELETE FROM {_nomeDaTabela} WHERE id = @id";
        
        using (var connection = _dbSession._connection)
        {
            var linhasAfetadas = await connection.ExecuteAsync(sql, new { id });
            return linhasAfetadas > 0;
        }
    }

}