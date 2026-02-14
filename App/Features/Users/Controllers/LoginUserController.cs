using App.Data.Context;
using App.Features.Users.Auth;
using App.Features.Users.Commands;
using App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Features.Users.Controllers;

[ApiController]
[Route("auth/user")]
[Tags("Auth")]
[AllowAnonymous]
public sealed class LoginUserController(AppDbContext context) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> ExecuteAsync([FromBody] LoginUserCommand login, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var user = await context.Users.SingleOrDefaultAsync(u => u.Email == login.Email, cancellationToken);

        if (user is null || !Hasher.VerifyHash(user.Password, login.Password))
            return Unauthorized(new { message = "Credênciais incorretas." });

        return Ok(new { message = "Login realizado com sucesso.", token = user.GenerateToken() });
    }
}