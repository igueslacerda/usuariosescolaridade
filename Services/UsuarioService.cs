namespace UsuariosEscolaridade.Services;

using Models;
using UsuariosEscolaridade.Helpers;

public interface IUsuarioService
{
    IEnumerable<UsuarioModel> ObterTodos();
    UsuarioModel ObterPorId(int id);
}

public class UsuarioService : IUsuarioService
{
    private DataContext _context;

    public UsuarioService(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<UsuarioModel> ObterTodos()
    {
        return _context.Usuarios.Select(u => new UsuarioModel(u.Id, u.Nome, new EscolaridadeModel(u.Escolaridade)));
    }

    public UsuarioModel ObterPorId(int id)
    {
        var usuario = _context.Usuarios.Where(u => u.Id == id)?.
                      Select(u => new UsuarioModel(u.Id, u.Nome, new EscolaridadeModel(u.Escolaridade)))?.
                      FirstOrDefault();

        if (usuario == null) throw new KeyNotFoundException("Usuário não encontrado");

        return usuario;              
    }
}
