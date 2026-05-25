using Domain.Command.Request;
using Microsoft.AspNetCore.Mvc;
using Service.NotificationService;
using TesteTecnicoCartSys.ControllerExtension;

[ApiController]
[Route("[controller]")]
public class LinguagemProgramacaoController : CartSysController
{
    private readonly ILinguagemProgramacaoService _service;

    public LinguagemProgramacaoController(INotifications notifications, ILinguagemProgramacaoService service) : base(notifications)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() 
        => CartSysResponse(await _service.GetAllLinguagensAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) 
        => CartSysResponse(await _service.GetLinguagemByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LinguagemProgramacaoRequest request)
    {
        if (request == null) 
            return BadRequest("Dados inválidos.");

        return CartSysResponse(await _service.CreateLinguagemAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] LinguagemProgramacaoRequest request)
    {
        if (request == null) 
            return BadRequest("Dados inconsistentes.");

        await _service.UpdateLinguagemAsync(id, request);
        return CartSysResponse();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteLinguagemAsync(id);
        return CartSysResponse();
    }
}