using UsuariosEscolaridade.Entities;

namespace UsuariosEscolaridade.Models;

public class EscolaridadeModel
{
    public int Id { get; set; }
    public string Descricao { get; set; }

    public EscolaridadeModel(){}
    public EscolaridadeModel(int id, string descricao)
    {
        Id = id;
        Descricao = descricao;
    }
    public EscolaridadeModel(EscolaridadeEntity entity)
    {
        Id = entity.Id;
        Descricao = entity.Descricao;
    }
}