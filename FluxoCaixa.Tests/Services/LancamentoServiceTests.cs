using FluxoCaixa.Application.Services;
using FluxoCaixa.Domain.Enums;
using FluxoCaixa.Messaging.Eventos;
using FluxoCaixa.Messaging.Interfaces;
using FluxoCaixa.Tests.Builders;
using FluentAssertions;
using Moq;

namespace FluxoCaixa.Tests.Services;

public class LancamentoServiceTests
{
    [Fact]
    public async Task RegistrarLancamento_DeveSalvarENotificarNaFila()
    {
        var context = DbContextBuilder.Create();
        var mockBus = new Mock<IBusPublisher>();
        var service = new LancamentoService(context, mockBus.Object);

        var data = DateTime.Today;
        var valor = 200m;
        var tipo = TipoLancamento.Credito;

        await service.RegistrarLancamentoAsync(data, valor, tipo);

        context.Lancamentos.Should().ContainSingle();
        var lanc = context.Lancamentos.First();
        lanc.Data.Should().Be(data);
        lanc.Valor.Should().Be(valor);
        lanc.Tipo.Should().Be(tipo);

        mockBus.Verify(b => b.PublicarLancamento(It.Is<LancamentoEvent>(e =>
            e.Data == data && e.Valor == valor && e.Tipo == tipo.ToString()
        )), Times.Once);
    }
}
