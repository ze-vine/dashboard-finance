using backend_csharp.Interfaces;
using backend_csharp.Models;
using Dapper;
using MySqlConnector;

namespace backend_csharp.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly string _connectionString;

    public UsuarioRepositorio(IConfiguration configuracao)
    {
        _connectionString = configuracao.GetConnectionString("DefaultConnection")!;
    }

    public async Task<IEnumerable<Usuario>> ListarTodos()
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.QueryAsync<Usuario>("SELECT id, nome, sobrenome, email, senha, logradouro, numero_do_logradouro AS NumeroDoLogradouro, cidade, estado, cep, telefone_principal AS TelefonePrincipal, telefone_secundario AS TelefoneSecundario FROM usuarios");
    }
}