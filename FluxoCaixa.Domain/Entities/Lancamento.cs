using FluxoCaixa.Domain.Enums;

namespace FluxoCaixa.Domain.Entities;

public class Lancamento
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public TipoLancamento Tipo { get; set; }
}
