using FluxoCaixa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FluxoCaixa.Domain.Enums;

namespace FluxoCaixa.Application.Services;

public class ConsolidadoService
{
    private readonly AppDbContext _context;

    public ConsolidadoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> CalcularSaldoDiarioAsync(DateTime data)
    {
        var lancamentos = await _context.Lancamentos
            .Where(l => l.Data == data.Date)
            .ToListAsync();

        var creditos = lancamentos
            .Where(l => l.Tipo == TipoLancamento.Credito)
            .Sum(l => l.Valor);

        var debitos = lancamentos
            .Where(l => l.Tipo == TipoLancamento.Debito)
            .Sum(l => l.Valor);

        return creditos - debitos;
    }
}
