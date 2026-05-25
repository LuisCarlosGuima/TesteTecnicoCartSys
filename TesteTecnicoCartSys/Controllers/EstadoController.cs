using Domain.Command.Request;
using Microsoft.AspNetCore.Mvc;
using Service.NotificationService;
using TesteTecnicoCartSys.ControllerExtension;

[ApiController]
[Route("[controller]")]
public class EstadoController : CartSysController
{
    private readonly IEstadoService _estadoService;

    public EstadoController(INotifications notifications, IEstadoService estadoService) : base(notifications)
    {
        _estadoService = estadoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() 
        => CartSysResponse(await _estadoService.GetAllEstadosAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) 
        => CartSysResponse(await _estadoService.GetEstadoByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EstadoRequest request)
    {
        if (request == null) 
            return BadRequest("Dados inválidos.");

        return CartSysResponse(await _estadoService.CreateEstadoAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EstadoRequest request)
    {
        if (request == null) 
            return BadRequest("Dados inconsistentes.");

        await _estadoService.UpdateEstadoAsync(id, request);
        return CartSysResponse();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _estadoService.DeleteEstadoAsync(id);
        return CartSysResponse();
    }
}