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
    void Criar(UsuarioNovoRequest usuario);
    UsuarioModel Atualizar(UsuarioAlterarRequest usuarioAlteracao);
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
        return _context.Usuarios.
                Select(u => new UsuarioModel(
                    u.Id,
                    u.Nome,
                    u.Sobrenome,
                    u.Email,
                    u.DataNascimento,
                    _mapper.Map<EscolaridadeModel>(u.Escolaridade)
                ));
    }

    public UsuarioModel ObterPorId(int id)
    {
        var usuarioSelect = _context.Usuarios.Where(u => u.Id == id)?.
                            Select(u => new { u, u.Escolaridade })?.FirstOrDefault();

        if (usuarioSelect == null) throw new KeyNotFoundException("Usuário não encontrado");

        var usuario = _mapper.Map<UsuarioModel>(usuarioSelect.u);
        usuario.Escolaridade = _mapper.Map<EscolaridadeModel>(usuarioSelect.Escolaridade);
        return usuario;              
    }

    public void Criar(UsuarioNovoRequest usuario)
    {
        //Validações
        if (usuario.Email != usuario.EmailConfirmacao)
            throw new AppException("O email de confirmação não confere.");

        if (_context.Usuarios.Any(u => u.Email == usuario.Email))
            throw new AppException($"Já existe um usuário com o email {usuario.Email}.");

        if (!_context.Escolaridade.Any(ec => ec.Id == usuario.EscolaridadeId))
            throw new KeyNotFoundException("A escolaridade selecionada não existe.");

        //Salvar o registro em banco
        var novoUsuario = _mapper.Map<UsuarioEntity>(usuario);

        _context.Usuarios.Add(novoUsuario);
        _context.SaveChanges();
    }

    public UsuarioModel Atualizar(UsuarioAlterarRequest usuarioAlteracao)
    {
        //Validações
        if (usuarioAlteracao.Email != null)
        {
            if (usuarioAlteracao.Email != usuarioAlteracao.EmailConfirmacao)
                throw new AppException("O email de confirmação não confere.");

            if (_context.Usuarios.Any(u => u.Id != usuarioAlteracao.Id && u.Email == usuarioAlteracao.Email))
                throw new AppException($"Já existe um usuário com o email {usuarioAlteracao.Email}.");
        }

        if (usuarioAlteracao.EscolaridadeId != null && !_context.Escolaridade.Any(ec => ec.Id == usuarioAlteracao.EscolaridadeId))
            throw new KeyNotFoundException("A escolaridade selecionada não existe.");

        var usuario = _context.Usuarios.Find(usuarioAlteracao.Id);

        if (usuario == null)
            throw new KeyNotFoundException("Usuário não encontrado.");

        //Aplicando alterações
        usuario = _mapper.Map(usuarioAlteracao, usuario);
        _context.Usuarios.Update(usuario);
        _context.SaveChanges();

        return ObterPorId(usuario.Id);
    }
}
