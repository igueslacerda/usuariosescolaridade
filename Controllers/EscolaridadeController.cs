namespace UsuariosEscolaridade.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services;

[ApiController]
[Route("[controller]")]
public class EscolaridadeController : ControllerBase
{
    private IEscolaridadeService _escolaridadeService;

    public EscolaridadeController(IEscolaridadeService escolaridadeService)
    {
        _escolaridadeService = escolaridadeService;
    }

    [HttpGet]
    public IActionResult ObterTodos()
    {
        var escolaridades = _escolaridadeService.ObterTodos();
        return Ok(escolaridades);
    }

    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        var escolaridade = _escolaridadeService.ObterPorId(id);
        return Ok(escolaridade);
    }
}