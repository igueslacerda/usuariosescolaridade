namespace UsuariosEscolaridade.Entities;

using System.ComponentModel.DataAnnotations;

public class EscolaridadeEntity
{
    [Key]
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public virtual ICollection<UsuarioEntity> Usuarios { get; set; }
}