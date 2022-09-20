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
    public IActionResult Criar(UsuarioNovoRequest usuario)
    {
        _usuarioService.Criar(usuario);
        return Ok(new { mensagem = "Usuário criado" });
    }

    [HttpPut]
    public IActionResult Atualizar(UsuarioAlterarRequest usuarioAlteracao)
    {
        var usuario = _usuarioService.Atualizar(usuarioAlteracao);
        return Ok(new { mensagem = $"Usuário {usuario.Nome} {usuario.Sobrenome} atualizado com sucesso." });
    }

    [HttpDelete("{id}")]
    public IActionResult Excluir(int id)
    {
        var usuario = _usuarioService.Excluir(id);
        return Ok(new { mensagem = $"Usuário {usuario.Nome} {usuario.Sobrenome} excluido com sucesso." });
    }
}