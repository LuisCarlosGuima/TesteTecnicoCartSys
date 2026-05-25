using Domain.Command.Request;
using Microsoft.AspNetCore.Mvc;
using Service.DesenvolvedorServices;
using Service.NotificationService;
using TesteTecnicoCartSys.ControllerExtension;

[ApiController]
[Route("[controller]")]
public class CidadeController : CartSysController
{
    private readonly ICidadeService _cidadeService;

    public CidadeController(INotifications notifications, ICidadeService cidadeService) : base(notifications)
    {
        _cidadeService = cidadeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() 
        => CartSysResponse(await _cidadeService.GetAllCidadesAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) 
        => CartSysResponse(await _cidadeService.GetCidadeByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CidadeRequest request)
    {
        if (request == null) 
            return BadRequest("Dados inválidos.");

        return CartSysResponse(await _cidadeService.CreateCidadeAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CidadeRequest request)
    {
        if (request == null) 
            return BadRequest("Dados inválidos.");

        await _cidadeService.UpdateCidadeAsync(id, request);
        return CartSysResponse();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _cidadeService.DeleteCidadeAsync(id);
        return CartSysResponse();
    }
}