using Domain.Command.Request;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DesenvolvedorServices;
using Service.NotificationService;
using TesteTecnicoCartSys.ControllerExtension;

namespace TesteTecnicoCartSys.Controllers;

[ApiController]
[Route("[controller]")]
public class DesenvolvedorController : CartSysController
{
    private readonly IDesenvolvedorRepository _desenvolvedorRepository;
    private readonly IDesenvolvedorService _desenvolvedorService;
    
    private readonly ICidadeRepository _cidadeRepository;
    private readonly IEstadoRepository _estadoRepository;

    public DesenvolvedorController(INotifications notifications,
                                   IDesenvolvedorRepository desenvolvedorRepository,
                                   IDesenvolvedorService desenvolvedorService,
                                   ICidadeRepository cidadeRepository,
                                   IEstadoRepository estadoRepository) : base(notifications)
    {
        _desenvolvedorRepository = desenvolvedorRepository;
        _desenvolvedorService = desenvolvedorService;
        _cidadeRepository = cidadeRepository;
        _estadoRepository = estadoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Desenvolvedor>>> GetAll()
    {
        var desenvolvedores = await _desenvolvedorService.GetAllDesenvolvedoresAsync();
        return CartSysResponse(desenvolvedores);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Desenvolvedor>> GetById(int id)
    {
        var desenvolvedor = await _desenvolvedorService.GetDesenvolvedorByIdAsync(id);
        return CartSysResponse(desenvolvedor);
    }

    [HttpPost]
    public async Task<ActionResult<Desenvolvedor>> Create([FromBody] DesenvolvedorRequest request)
    {
        if (request == null) 
            return BadRequest("Dados inválidos.");
        var novoDev = await _desenvolvedorService.CreateDesenvolvedorAsync(request);
        return CartSysResponse(novoDev);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DesenvolvedorRequest request)
    {
        if (request == null) 
            return BadRequest("Dados inconsistentes.");

        await _desenvolvedorService.UpdateDesenvolvedorAsync(id, request);
        return CartSysResponse();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var desenvolvedor = await _desenvolvedorRepository.GetByIdAsync(id);
        if (desenvolvedor == null)
        {
            return NotFound($"Desenvolvedor com ID {id} não encontrado.");
        }

        await _desenvolvedorRepository.RemoveAsync(desenvolvedor);
        return NoContent();
    }

    [HttpGet("relatorio")]
    public IActionResult GerarRelatorio()
    {
        var pdfBytes = _desenvolvedorService.GerarRelatorioPdf();
        return CartSysResponse(File(pdfBytes, "application/pdf", "Relatorio_Desenvolvedores.pdf"));
    }
}