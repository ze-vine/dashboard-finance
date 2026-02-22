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

    public UsuarioRepositorio(DbSession dbSession)
    {
        _dbSession = dbSession;
    }

    public async Task<IEnumerable<Usuario>> ListarUsuariosAsync()
    {
        string sql = "SELECT * FROM usuarios";
        using (var connection = _dbSession._connection)
        {
            return await connection.QueryAsync<Usuario>(sql);
        }
    }

    public async Task<Usuario> obterUsuarioAsync(int id)
    {
        string sql = "SELECT * FROM usuarios WHERE id = @Id";
        using (var connection = _dbSession._connection)
        {
            return await connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
        }
    }

    public async Task<int> AdicionarUsuarioAsync(Usuario usuario)
    {
        string nomeDaTabela = "usuarios";

        string sql = $@"INSERT INTO {nomeDaTabela} 
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

}