using FluxoCaixa.Domain.Enums;

namespace FluxoCaixa.Application.DTOs;

public class LancamentoRequest
{
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public TipoLancamento Tipo { get; set; }
}
