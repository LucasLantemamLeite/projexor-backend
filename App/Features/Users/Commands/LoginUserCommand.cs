using System.ComponentModel.DataAnnotations;

namespace App.Features.Users.Commands;

public sealed record LoginUserCommand
{
    [Required(ErrorMessage = "O campo de email é obrigatório.")]
    [StringLength(255, ErrorMessage = "O campo de email deve menos de 255 caracteres.")]
    [EmailAddress(ErrorMessage = "O campo de email é inválido.")]
    public required string Email { get; init; }

    [Required(ErrorMessage = "O campo de senha é obrigatório.")]
    [StringLength(30, ErrorMessage = "O campo se senha deve ter menos de 30 caracteres.")]
    public required string Password { get; init; }
}