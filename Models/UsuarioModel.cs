namespace UsuariosEscolaridade.Models;

public class UsuarioModel
{
    public UsuarioModel()
    {
    }

    public UsuarioModel(int id, string nome, string sobrenome, string email, DateTime? dataNascimento, EscolaridadeModel escolaridade)
    {
        Id = id;
        Nome = nome;
        Sobrenome = sobrenome;
        Email = email;
        DataNascimento = dataNascimento;
        Escolaridade = escolaridade;
    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Email { get; set; }
    public DateTime? DataNascimento { get; set; }
    public EscolaridadeModel Escolaridade { get; set; }
}