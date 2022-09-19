namespace UsuariosEscolaridade.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services;
using UsuariosEscolaridade.Services.Requests;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public IActionResult ObterTodos()
    {
        var usuarios = _usuarioService.ObterTodos();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        var usuarios = _usuarioService.ObterPorId(id);
        return Ok(usuarios);
    }

    [HttpPost]
    public IActionResult Criar(NovoUsuarioRequest usuario)
    {
        _usuarioService.Criar(usuario);
        return Ok(new { mensagem = "Usuário criado" });
    }
}