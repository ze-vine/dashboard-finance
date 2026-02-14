namespace backend_csharp.Models;

public class Usuario
{
    public int Id { get; init; }
    public string Nome { get; set; } = string.Empty;
    public string Sobrenome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string Logradouro { get; set; } = string.Empty;
    public int NumeroDoLogradouro { get; set; }
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
    public string TelefonePrincipal { get; set; } = string.Empty;
    public string TelefoneSecundario { get; set; } = string.Empty;
}