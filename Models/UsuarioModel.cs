namespace UsuariosEscolaridade.Models;

public class UsuarioModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    //public string Sobrenome { get; set; }
    public string Email { get; set; }
    //public DateOnly DataNascimento { get; set; }
    public EscolaridadeModel Escolaridade { get; set; }

    
    public UsuarioModel(){}
    public UsuarioModel(int id, 
                        string nome,
                        //string sobrenome,
                        string email,
                        //DateOnly dataNascimento,
                        EscolaridadeModel escolaridade)
    {
        Id = id;
        Nome = nome;
        Escolaridade = escolaridade;
    }
}