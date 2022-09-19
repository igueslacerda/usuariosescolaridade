namespace UsuariosEscolaridade.Entities;

public class EscolaridadeEntity
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public virtual ICollection<UsuarioEntity>? Usuarios { get; set; }
}