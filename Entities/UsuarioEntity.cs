namespace UsuariosEscolaridade.Entities;
public class UsuarioEntity
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public int EscolaridadeId { get; set; }
    public EscolaridadeEntity? Escolaridade { get; set; }
}