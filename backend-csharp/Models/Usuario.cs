using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_csharp.Models;

[Table("usuarios")]
public class Usuario
{
    [Key]
    [Column("id")]
    public int Id { get; init; }
    [Column("nome")]
    public string Nome { get; set; }
    [Column("sobrenome")]
    public string Sobrenome { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("senha")]
    public string Senha { get; set; }
    [Column("logradouro")]
    public string Logradouro { get; set; }
    [Column("numero_do_logradouro")]
    public int NumeroDoLogradouro { get; set; }
    [Column("cidade")]
    public string Cidade { get; set; }
    [Column("estado")]
    public string Estado { get; set; }
    [Column("cep")]
    public string Cep { get; set; }
    [Column("telefone_principal")]
    public string TelefonePrincipal { get; set; }
    [Column("telefone_secundario")]
    public string TelefoneSecundario { get; set; }
}