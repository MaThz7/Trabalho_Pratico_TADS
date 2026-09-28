using Backend.Data;
using Backend.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AluguelController : ControllerBase
{
    private readonly LocadoraContext _context;

    public AluguelController(LocadoraContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var alugueis = await _context.Alugueis.ToListAsync();

        return Ok(alugueis);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var aluguel = await _context.Alugueis.FindAsync(id);

        if (aluguel == null)
        {
            return NotFound();
        }

        return Ok(aluguel);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(Aluguel aluguel)
    {
        _context.Alugueis.Add(aluguel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPorId),
            new { id = aluguel.Id },
            aluguel
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, Aluguel aluguel)
    {
        if (id != aluguel.Id)
        {
            return BadRequest();
        }

        _context.Entry(aluguel).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var aluguel = await _context.Alugueis.FindAsync(id);

        if (aluguel == null)
        {
            return NotFound();
        }

        _context.Alugueis.Remove(aluguel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("cliente/{id}")]
    public async Task<IActionResult> GetPorCliente(int id)
    {
        var alugueis = await _context.Alugueis
            .Where(a => a.ClienteId == id)
            .Select(a => new
            {
                a.Id,
                a.ClienteId,
                a.VeiculoId,
                a.FormaPagamentoId,
                a.DataInicio,
                a.DataFim,
                a.DataDevolucao,
                a.QuilometragemInicial,
                a.QuilometragemFinal,
                a.ValorDiaria,
                a.ValorTotal
            })
            .ToListAsync();

        return Ok(alugueis);
    }

    [HttpGet("veiculo/{id}")] public async Task<IActionResult> GetPorVeiculo(int id) { var alugueis = await _context.Alugueis.Where(a => a.VeiculoId == id).Select(a => new { a.Id, a.ClienteId, a.VeiculoId, a.FormaPagamentoId, a.DataInicio, a.DataFim, a.DataDevolucao, a.QuilometragemInicial, a.QuilometragemFinal, a.ValorDiaria, a.ValorTotal }).ToListAsync(); return Ok(alugueis); }


    [HttpGet("forma-pagamento/{id}")]
    public async Task<IActionResult> GetPorFormaPagamento(int id)
    {
        var alugueis = await _context.Alugueis
            .Where(a => a.FormaPagamentoId == id)
            .Select(a => new
            {
                a.Id,
                a.ClienteId,
                a.VeiculoId,
                a.FormaPagamentoId,
                a.DataInicio,
                a.DataFim,
                a.DataDevolucao,
                a.QuilometragemInicial,
                a.QuilometragemFinal,
                a.ValorDiaria,
                a.ValorTotal
            })
            .ToListAsync();

        return Ok(alugueis);
    }

}