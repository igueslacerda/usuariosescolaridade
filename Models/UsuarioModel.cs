namespace UsuariosEscolaridade.Models;

public class UsuarioModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public EscolaridadeModel Escolaridade { get; set; }

    
    public UsuarioModel(){}
    public UsuarioModel(int id, string nome, EscolaridadeModel escolaridade)
    {
        Id = id;
        Nome = nome;
        Escolaridade = escolaridade;
    }
}