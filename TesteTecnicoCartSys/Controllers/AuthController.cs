using Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Service.NotificationService;
using TesteTecnicoCartSys.ControllerExtension;

[Route("auth")]
[AllowAnonymous]
public class AuthController : CartSysController
{
    private readonly IAuthService _authService;

    public AuthController(
        INotifications notifications,
        IAuthService authService) : base(notifications)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authService.Login(request.Email, request.Senha);
        return CartSysResponse(response);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var response = await _authService.Refresh(request.RefreshToken);
        return CartSysResponse(response);
    }
}