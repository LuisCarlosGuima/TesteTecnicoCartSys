using Domain.Command.Request;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.NotificationService;
using TesteTecnicoCartSys.ControllerExtension;

[ApiController]
[Route("[controller]")]
public class UsuarioController : CartSysController
{
    private readonly IUsuarioService _service;

    public UsuarioController(INotifications notifications, IUsuarioService service) : base(notifications)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => CartSysResponse(await _service.GetAllUsuariosAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => CartSysResponse(await _service.GetUsuarioByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Usuario usuario)
    {
        if (usuario == null) return BadRequest("Dados do usuário inválidos.");
        return CartSysResponse(await _service.CreateUsuarioAsync(usuario));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UsuarioRequest request)
    {
        if (request == null) return BadRequest("Dados inválidos.");
        await _service.UpdateUsuarioAsync(id, request);
        return CartSysResponse();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteUsuarioAsync(id);
        return CartSysResponse();
    }
}