using Backend.Data;
using Backend.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormaPagamentoController : ControllerBase
{
    private readonly LocadoraContext _context;

    public FormaPagamentoController(LocadoraContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var formasPagamento = await _context.FormasPagamento.ToListAsync();

        return Ok(formasPagamento);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var formaPagamento = await _context.FormasPagamento.FindAsync(id);

        if (formaPagamento == null)
        {
            return NotFound();
        }

        return Ok(formaPagamento);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(FormaPagamento formaPagamento)
    {
        _context.FormasPagamento.Add(formaPagamento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPorId),
            new { id = formaPagamento.Id },
            formaPagamento
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(
        int id,
        FormaPagamento formaPagamento)
    {
        if (id != formaPagamento.Id)
        {
            return BadRequest();
        }

        _context.Entry(formaPagamento).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var formaPagamento = await _context.FormasPagamento.FindAsync(id);

        if (formaPagamento == null)
        {
            return NotFound();
        }

        _context.FormasPagamento.Remove(formaPagamento);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}