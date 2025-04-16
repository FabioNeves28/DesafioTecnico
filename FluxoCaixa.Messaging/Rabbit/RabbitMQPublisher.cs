using FluxoCaixa.Messaging.Eventos;
using FluxoCaixa.Messaging.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FluxoCaixa.Messaging.Rabbit;

public class RabbitMQPublisher : IBusPublisher
{
    private readonly IModel _channel;

    public RabbitMQPublisher()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();

        _channel.QueueDeclare(queue: "lancamentos",
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
    }

    public void PublicarLancamento(LancamentoEvent lancamento)
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(lancamento));

        _channel.BasicPublish(exchange: "",
                              routingKey: "lancamentos",
                              basicProperties: null,
                              body: body);
    }
}
