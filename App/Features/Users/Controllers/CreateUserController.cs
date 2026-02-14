using App.Data.Context;
using App.Features.Users.Auth;
using App.Features.Users.Commands;
using App.Features.Users.Models;
using App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Features.Users.Controllers;

[ApiController]
[Route("create/user")]
[Tags("Create")]
[AllowAnonymous]
public sealed class CreateUserController(AppDbContext context) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> ExecuteAsync([FromBody] CreateUserCommand create, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        if (await context.Users.AnyAsync(u => u.Email == create.Email, cancellationToken))
            return Conflict(new { message = "Email já cadastrado. Tente novamente." });

        if (await context.Users.AnyAsync(u => u.Phone == create.Phone, cancellationToken))
            return Conflict(new { message = "Telefone já cadastrado. Tente novamente." });

        var user = new User(
            name: create.Name,
            email: create.Email,
            password: Hasher.GenerateHash(create.Password),
            phone: create.Phone
        );

        context.Users.Add(user);

        await context.SaveChangesAsync(cancellationToken);

        return Created("", new { message = "Conta criada com sucesso.", token = user.GenerateToken() });
    }
}