namespace UsuariosEscolaridade.Entities;
public class UsuarioEntity
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    public string? Email { get; set; }
    public DateTime? DataNascimento { get; set; }
    public int EscolaridadeId { get; set; }
    public EscolaridadeEntity Escolaridade { get; set; }
}