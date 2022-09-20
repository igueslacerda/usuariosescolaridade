namespace UsuariosEscolaridade.Services;

using AutoMapper;
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
    private readonly IMapper _mapper;

    public EscolaridadeService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<EscolaridadeModel> ObterTodos()
    {
        var todasEscolaridades = _mapper.Map<IEnumerable<EscolaridadeModel>>(_context.Escolaridade);
        return todasEscolaridades;
    }

    public EscolaridadeModel ObterPorId(int id)
    {
        var escolaridade = _context.Escolaridade.Where(ec => ec.Id == id)?.
                           Select(ec => _mapper.Map<EscolaridadeModel>(ec))?.
                           FirstOrDefault();
        
        if (escolaridade == null) throw new KeyNotFoundException("Escolaridade não encontrada");

        return escolaridade;
    }
}