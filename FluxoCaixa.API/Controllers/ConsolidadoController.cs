using FluxoCaixa.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ConsolidadoController : ControllerBase
{
    private readonly ConsolidadoService _service;

    public ConsolidadoController(ConsolidadoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObterConsolidado([FromQuery] DateTime data)
    {
        var saldo = await _service.CalcularSaldoDiarioAsync(data);
        return Ok(new { Data = data.Date.ToString("yyyy-MM-dd"), Saldo = saldo });
    }
}
