namespace UsuariosEscolaridade.Models;

public class UsuarioModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public EscolaridadeModel Escolaridade { get; set; }
}