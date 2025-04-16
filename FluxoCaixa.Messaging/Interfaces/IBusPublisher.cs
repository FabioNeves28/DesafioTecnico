using FluxoCaixa.Messaging.Eventos;

namespace FluxoCaixa.Messaging.Interfaces;

public interface IBusPublisher
{
    void PublicarLancamento(LancamentoEvent lancamento);
}
