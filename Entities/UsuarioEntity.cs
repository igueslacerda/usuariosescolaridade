namespace UsuariosEscolaridade.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UsuarioEntity
{
    [Key]
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    public string? Email { get; set; }
    public DateTime? DataNascimento { get; set; }
    public int? EscolaridadeId { get; set; }

    [ForeignKey("EscolaridadeId")]
    public EscolaridadeEntity Escolaridade { get; set; }
}