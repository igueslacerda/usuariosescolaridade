using System.ComponentModel.DataAnnotations;

namespace UsuariosEscolaridade.Services.Requests;

public class NovoUsuarioRequest
{
    [Required]
    public string Nome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [EmailAddress]
    [Compare("Email")]
    public string EmailConfirmacao { get; set; }

    [Required]
    public int EscolaridadeId { get; set; }
}