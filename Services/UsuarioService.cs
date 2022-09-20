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
    UsuarioModel Excluir(int id);
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

        if (usuario.DataNascimento > DateTime.Now)
            throw new AppException("A data de nascimento não pode ser maior que hoje.");

        EscolaridadeEntity escolaridade;
        if (!string.IsNullOrEmpty(usuario.Escolaridade))
            escolaridade = _context.Escolaridade.FirstOrDefault(ec => ec.Descricao == usuario.Escolaridade);        
        else if (usuario.EscolaridadeId == null)
            throw new AppException("A escolaridade deve ser informada.");
        else
            escolaridade = _context.Escolaridade.Find(usuario.EscolaridadeId.Value);
        if (escolaridade == null)
            throw new KeyNotFoundException("A escolaridade selecionada não existe.");
        usuario.EscolaridadeId = escolaridade.Id;

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

        if (usuarioAlteracao.DataNascimento != null && usuarioAlteracao.DataNascimento > DateTime.Now)
            throw new AppException("A data de nascimento não pode ser maior que hoje.");

        if (!string.IsNullOrEmpty(usuarioAlteracao.Escolaridade) || usuarioAlteracao.EscolaridadeId != null)
        {
            EscolaridadeEntity escolaridade;
            if (!string.IsNullOrEmpty(usuarioAlteracao.Escolaridade))
                escolaridade = _context.Escolaridade.FirstOrDefault(ec => ec.Descricao == usuarioAlteracao.Escolaridade);        
            else
                escolaridade = _context.Escolaridade.Find(usuarioAlteracao.EscolaridadeId.Value);
            if (escolaridade == null)
                throw new KeyNotFoundException("A escolaridade selecionada não existe.");
            usuarioAlteracao.EscolaridadeId = escolaridade.Id;
        }

        var usuario = _context.Usuarios.Find(usuarioAlteracao.Id);

        if (usuario == null)
            throw new KeyNotFoundException("Usuário não encontrado.");

        //Aplicando alterações
        usuario = _mapper.Map(usuarioAlteracao, usuario);
        _context.Usuarios.Update(usuario);
        _context.SaveChanges();

        return ObterPorId(usuario.Id);
    }

    public UsuarioModel Excluir(int id)
    {
        var usuario = _context.Usuarios.Find(id);

        if (usuario == null)
            throw new KeyNotFoundException("Usuário não encontrado.");

        _context.Usuarios.Remove(usuario);
        _context.SaveChanges();

        return _mapper.Map<UsuarioModel>(usuario);
    }
}
