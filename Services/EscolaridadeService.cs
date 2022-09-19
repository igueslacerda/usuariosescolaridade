namespace UsuariosEscolaridade.Services;

using Models;
using UsuariosEscolaridade.Helpers;

public interface IEscolaridadeService
{
    IEnumerable<EscolaridadeModel> ObterTodos();
    EscolaridadeModel ObterPorId(int id);    
}

public class EscolaridadeService : IEscolaridadeService
{
    private DataContext _context;

    public EscolaridadeService(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<EscolaridadeModel> ObterTodos()
    {
        return _context.Escolaridade.Select(ec => new EscolaridadeModel(ec.Id, ec.Descricao));
    }

    public EscolaridadeModel ObterPorId(int id)
    {
        var escolaridade = _context.Escolaridade.Where(ec => ec.Id == id)?.
                           Select(ec => new EscolaridadeModel(ec.Id, ec.Descricao))?.
                           FirstOrDefault();
        
        if (escolaridade == null) throw new KeyNotFoundException("Escolaridade não encontrada");

        return escolaridade;
    }
}