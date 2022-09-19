namespace UsuariosEscolaridade.Services;

using Models;
using UsuariosEscolaridade.Entities;
using UsuariosEscolaridade.Helpers;
using UsuariosEscolaridade.Services.Requests;

public interface IUsuarioService
{
    IEnumerable<UsuarioModel> ObterTodos();
    UsuarioModel ObterPorId(int id);
    void Criar(NovoUsuarioRequest usuario);
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

    public void Criar(NovoUsuarioRequest usuario)
    {
        //Validações
        if (usuario.Email != usuario.EmailConfirmacao)
            throw new AppException("O email de confirmação não confere.");

        if (_context.Usuarios.Any(u => u.Email == usuario.Email))
            throw new AppException($"Já existe um usuário com o email {usuario.Email}.");

        if (!_context.Escolaridade.Any(ec => ec.Id == usuario.EscolaridadeId))
            throw new KeyNotFoundException("A escolaridade selecionada não existe.");

        //Salvar o registro em banco
        var novoUsuario = new UsuarioEntity
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
            EscolaridadeId = usuario.EscolaridadeId
        };

        _context.Usuarios.Add(novoUsuario);
        _context.SaveChanges();
    }
}
