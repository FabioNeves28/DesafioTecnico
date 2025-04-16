using FluxoCaixa.Domain.Entities;
using FluxoCaixa.Domain.Enums;
using FluxoCaixa.Infrastructure.Data;
using FluxoCaixa.Messaging;
using FluxoCaixa.Messaging.Eventos;
using FluxoCaixa.Messaging.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace FluxoCaixa.Application.Services;

public class LancamentoService
{
    private readonly AppDbContext _context;
    private readonly IBusPublisher _bus;

    public LancamentoService(AppDbContext context, IBusPublisher bus)
    {
        _context = context;
        _bus = bus;
    }

    public async Task RegistrarLancamentoAsync(DateTime data, decimal valor, TipoLancamento tipo)
    {
        var lancamento = new Lancamento
        {
            Data = data.Date,
            Valor = valor,
            Tipo = tipo
        };

        _context.Lancamentos.Add(lancamento);
        await _context.SaveChangesAsync();

        _bus.PublicarLancamento(new LancamentoEvent
        {
            Data = lancamento.Data,
            Valor = lancamento.Valor,
            Tipo = lancamento.Tipo.ToString()
        });
    }

    public async Task<decimal> ObterSaldoPorDataAsync(DateTime data)
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
