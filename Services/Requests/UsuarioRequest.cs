using System.ComponentModel.DataAnnotations;

namespace UsuariosEscolaridade.Services.Requests;

public class UsuarioNovoRequest
{
    [Required]
    public string Nome { get; set; }

    [Required]
    public string Sobrenome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [EmailAddress]
    [Compare("Email")]
    public string EmailConfirmacao { get; set; }

    [Required]
    public DateTime? DataNascimento { get; set; }

    [Required]
    public int EscolaridadeId { get; set; }
}

public class UsuarioAlterarRequest
{
    [Required]
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? Sobrenome { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [EmailAddress]
    [Compare("Email")]
    public string? EmailConfirmacao { get; set; }

    public DateTime? DataNascimento { get; set; }

    public int? EscolaridadeId { get; set; }
}