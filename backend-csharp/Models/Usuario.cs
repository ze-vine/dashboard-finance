using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_csharp.Models;

public class Usuario
{
    public int Id { get; init; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Logradouro { get; set; }
    public int NumeroDoLogradouro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Cep { get; set; }
    public string TelefonePrincipal { get; set; }
    public string TelefoneSecundario { get; set; }
}