using Backend.Data;
using Backend.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FabricanteController : ControllerBase
{
    private readonly LocadoraContext _context;

    public FabricanteController(LocadoraContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var fabricantes = await _context.Fabricantes.ToListAsync();

        return Ok(fabricantes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var fabricante = await _context.Fabricantes.FindAsync(id);

        if (fabricante == null)
        {
            return NotFound();
        }

        return Ok(fabricante);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(Fabricante fabricante)
    {
        _context.Fabricantes.Add(fabricante);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPorId),
            new { id = fabricante.Id },
            fabricante
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, Fabricante fabricante)
    {
        if (id != fabricante.Id)
        {
            return BadRequest();
        }

        _context.Entry(fabricante).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var fabricante = await _context.Fabricantes.FindAsync(id);

        if (fabricante == null)
        {
            return NotFound();
        }

        _context.Fabricantes.Remove(fabricante);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
