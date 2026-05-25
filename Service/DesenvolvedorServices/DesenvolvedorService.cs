using Domain.Contracts;
using Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Service.DesenvolvedorServices;
using Microsoft.OpenApi.Extensions;
using Domain.Command.Request;
using Service.NotificationService;

public class DesenvolvedorService : IDesenvolvedorService
{
    private readonly IDesenvolvedorRepository _desenvolvedorRepository;
    private readonly ILinguagemProgramacaoRepository _linguagemProgramacaoRepository;
    private readonly ICidadeRepository _cidadeRepository;
    private readonly IEstadoRepository _estadoRepository;
    private readonly INotifications _notifications;

    public DesenvolvedorService(
        IDesenvolvedorRepository desenvolvedorRepository,
        ILinguagemProgramacaoRepository linguagemProgramacaoRepository,
        ICidadeRepository cidadeRepository,
        IEstadoRepository estadoRepository,
        INotifications notifications)
    {
        _desenvolvedorRepository = desenvolvedorRepository;
        _linguagemProgramacaoRepository = linguagemProgramacaoRepository;
        _cidadeRepository = cidadeRepository;
        _estadoRepository = estadoRepository;
        _notifications = notifications;
    }
    public async Task<Desenvolvedor> GetDesenvolvedorByIdAsync(int id)
    {
        var desenvolvedor = await _desenvolvedorRepository.GetByIdAsync(id);

        if (desenvolvedor == null)
        {
            _notifications.AddError($"Desenvolvedor com ID {id} não encontrado.");
            return null;
        }

        return desenvolvedor;
    }

    public async Task<Desenvolvedor> CreateDesenvolvedorAsync(DesenvolvedorRequest request)
    {
        var dev = new Desenvolvedor
        {
            Nome = request.Nome,
            Email = request.Email,
            Senioridade = request.Senioridade,
            CidadeId = request.CidadeId,
            Observacoes = request.Observacoes
        };

        if (request.LinguagensIds != null && request.LinguagensIds.Any())
        {
            var linguagens = await _linguagemProgramacaoRepository.GetByManyIdsAsync(request.LinguagensIds);
            dev.Linguagens = linguagens.ToList();
        }

        var result = await _desenvolvedorRepository.AddAsync(dev);

        if (request is null)
            _notifications.AddError("Não foi possivel criar o desenvolvedor!");

        return result;

    }

    public async Task UpdateDesenvolvedorAsync(int id, DesenvolvedorRequest request)
    {
        var devExiste = await _desenvolvedorRepository.GetByIdWithLinguagensAsync(id);

        if (devExiste is null)
        {
            _notifications.AddError($"Desenvolvedor com ID {id} não encontrado.");
            return;
        }
        
        devExiste.Nome = request.Nome;
        devExiste.Email = request.Email;
        devExiste.Senioridade = request.Senioridade;
        devExiste.CidadeId = request.CidadeId;
        devExiste.Observacoes = request.Observacoes;

        devExiste.Linguagens.Clear();
        if (request.LinguagensIds != null && request.LinguagensIds.Any())
        {
            var linguagens = await _linguagemProgramacaoRepository.GetByManyIdsAsync(request.LinguagensIds);
            foreach (var lang in linguagens)
            {
                devExiste.Linguagens.Add(lang);
            }
        }

        var result = await _desenvolvedorRepository.UpdateAsync(devExiste);
        if (request is null)
            _notifications.AddError("Não foi possivel atualizar o desenvolvedor!");

    }

    public async Task<IEnumerable<Desenvolvedor>> GetAllDesenvolvedoresAsync()
    {
        return await _desenvolvedorRepository.GetAllWithRelationshipsAsync();
    }

    public byte[] GerarRelatorioPdf()
    {
        try
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var desenvolvedores = _desenvolvedorRepository.GetAllWithRelationshipsAsync().Result;
            var cidades = _cidadeRepository.GetAllAsync().Result;
            var estados = _estadoRepository.GetAllAsync().Result;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Text("Relatório de Desenvolvedores").SemiBold().FontSize(20).FontColor(Colors.Blue.Darken2);

                    page.Content().PaddingVertical(1, Unit.Centimetre).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            for (int i = 0; i < 5; i++) columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Nome").SemiBold();
                            header.Cell().Text("Cidade").SemiBold();
                            header.Cell().Text("Estado").SemiBold();
                            header.Cell().Text("Senioridade").SemiBold();
                            header.Cell().Text("Linguagens").SemiBold();
                        });

                        foreach (var dev in desenvolvedores)
                        {
                            var cidade = cidades.FirstOrDefault(c => c.Id == dev.CidadeId);
                            var estado = cidade != null ? estados.FirstOrDefault(e => e.Id == cidade.EstadoId) : null;
                            var linguagens = dev.Linguagens != null && dev.Linguagens.Any() ? string.Join(", ", dev.Linguagens.Select(l => l.Nome)) : "Nenhuma";
                            var senioridade = dev.Senioridade.GetDisplayName();

                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(2).Text(dev.Nome);
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(2).Text(cidade?.Nome ?? "-");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(2).Text(estado?.Sigla ?? "-");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(2).Text(senioridade);
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(2).Text(linguagens);
                        }
                    });

                    page.Footer().AlignCenter().Text(x => { x.Span("Página "); x.CurrentPageNumber(); });
                });
            });

            return document.GeneratePdf();
        }
        catch(Exception e)
        {
            _notifications.AddError($"Erro ao gerar o PDF. Message: {e.Message}");
            return null;
        }
    }
}