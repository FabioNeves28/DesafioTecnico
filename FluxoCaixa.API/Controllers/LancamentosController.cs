using FluxoCaixa.Application.Services;
using FluxoCaixa.Domain.Enums;
using FluxoCaixa.Application;
using Microsoft.AspNetCore.Mvc;
using FluxoCaixa.Application.DTOs;

namespace FluxoCaixa.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LancamentosController : ControllerBase
{
    private readonly LancamentoService _service;

    public LancamentosController(LancamentoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Registrar([FromBody] LancamentoRequest request)
    {
        if (request.Valor <= 0)
            return BadRequest("O valor deve ser maior que zero.");

        await _service.RegistrarLancamentoAsync(request.Data, request.Valor, request.Tipo);
        return Ok("Lançamento registrado com sucesso.");
    }

    [HttpGet("saldo")]
    public async Task<IActionResult> ObterSaldo([FromQuery] DateTime data)
    {
        var saldo = await _service.ObterSaldoPorDataAsync(data);
        return Ok(new { Data = data.Date.ToString("yyyy-MM-dd"), Saldo = saldo });
    }
}
