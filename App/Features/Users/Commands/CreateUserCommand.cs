using System.ComponentModel.DataAnnotations;

namespace App.Features.Users.Commands;

public sealed record CreateUserCommand
{
    [Required(ErrorMessage = "O campo de nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo de nome deve ter no máximo 100 caracteres.")]
    public required string Name { get; init; }

    [Required(ErrorMessage = "O campo de email é obrigatório.")]
    [StringLength(255, ErrorMessage = "O campo de email deve menos de 255 caracteres.")]
    [EmailAddress(ErrorMessage = "O campo de email é inválido.")]
    public required string Email { get; init; }

    [Required(ErrorMessage = "O campo de senha é obrigatório.")]
    [StringLength(30, ErrorMessage = "O campo se senha deve ter menos de 30 caracteres.")]
    public required string Password { get; init; }

    [Required(ErrorMessage = "O campo de telefone é obrigatório.")]
    [StringLength(20, ErrorMessage = "O campo de senha deve ter menos de 20 caracteres.")]
    public required string Phone { get; init; }
}