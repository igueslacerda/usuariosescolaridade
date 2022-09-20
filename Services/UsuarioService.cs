namespace UsuariosEscolaridade.Services;

using AutoMapper;
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
    private readonly IMapper _mapper;

    public UsuarioService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<UsuarioModel> ObterTodos()
    {
        var todosUsuarios = _mapper.Map<List<UsuarioModel>>(_context.Usuarios);
        return todosUsuarios;
    }

    public UsuarioModel ObterPorId(int id)
    {
        var usuarioEntity = _context.Usuarios.Where(u => u.Id == id)?.FirstOrDefault();
        var usuario = _mapper.Map<UsuarioModel>(usuarioEntity);        

        if (usuario == null) throw new KeyNotFoundException("Usuário não encontrado");

        var escEntity = usuarioEntity.Escolaridade;
        usuario.Escolaridade = _mapper.Map<EscolaridadeModel>(escEntity);

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
