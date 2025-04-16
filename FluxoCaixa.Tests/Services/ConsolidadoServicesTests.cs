using FluxoCaixa.Application.Services;
using FluxoCaixa.Domain.Enums;
using FluxoCaixa.Messaging.Interfaces;
using FluxoCaixa.Tests.Builders;
using FluentAssertions;
using Moq;

namespace FluxoCaixa.Tests.Services;

public class ConsolidadoServiceTests
{
    [Fact]
    public async Task ObterSaldoPorData_DeveRetornarSaldoCorreto()
    {
        var context = DbContextBuilder.Create();
        var mockBus = new Mock<IBusPublisher>();
        var service = new LancamentoService(context, mockBus.Object);

        var data = new DateTime(2024, 1, 1);

        await service.RegistrarLancamentoAsync(data, 100, TipoLancamento.Credito);
        await service.RegistrarLancamentoAsync(data, 30, TipoLancamento.Debito);
        await service.RegistrarLancamentoAsync(data, 70, TipoLancamento.Credito);

        var saldo = await service.ObterSaldoPorDataAsync(data);

        saldo.Should().Be(140); // 100 + 70 - 30
    }
}
