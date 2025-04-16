namespace FluxoCaixa.Messaging.Eventos;

public class LancamentoEvent
{
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public string Tipo { get; set; } = string.Empty;
}
