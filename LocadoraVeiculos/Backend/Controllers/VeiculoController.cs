using Backend.Data;
using Backend.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]


public class VeiculoController : ControllerBase
{
    private readonly LocadoraContext _context;

    public VeiculoController(LocadoraContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var veiculos = await _context.Veiculos.ToListAsync();

        return Ok(veiculos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var veiculo = await _context.Veiculos.FindAsync(id);

        if (veiculo == null)
        {
            return NotFound();
        }

        return Ok(veiculo);
    }


    [HttpPost]
    public async Task<IActionResult> Criar(Veiculo veiculo)
    {
        _context.Veiculos.Add(veiculo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPorId), new { id = veiculo.Id }, veiculo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, Veiculo veiculo)
    {
        if (id != veiculo.Id)
        {
            return BadRequest();
        }

        _context.Entry(veiculo).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var veiculo = await _context.Veiculos.FindAsync(id);

        if (veiculo == null)
        {
            return NotFound();
        }

        _context.Veiculos.Remove(veiculo);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpGet("fabricante/{nome}")]
    public async Task<IActionResult> GetPorFabricante(string nome)
    {
        var veiculos = await (
            from v in _context.Veiculos
            join f in _context.Fabricantes
                on v.FabricanteId equals f.Id
            where f.Nome == nome
            select new
            {
                v.Id,
                v.Modelo,
                v.AnoFabricacao,
                v.Quilometragem,
                v.FabricanteId,
                v.CategoriaId,
                Fabricante = f.Nome
            }
        ).ToListAsync();

        return Ok(veiculos);
    }


    [HttpGet("categoria/{nome}")]
    public async Task<IActionResult> GetPorCategoria(string nome)
    {
        var veiculos = await _context.Veiculos
            .GroupJoin(
                _context.Categorias,
                v => v.CategoriaId,
                c => c.Id,
                (v, categorias) => new { v, categorias }
            )
            .SelectMany(
                x => x.categorias.DefaultIfEmpty(),
                (x, c) => new
                {
                    x.v.Id,
                    x.v.Modelo,
                    x.v.AnoFabricacao,
                    x.v.Quilometragem,
                    x.v.FabricanteId,
                    x.v.CategoriaId,
                    Categoria = c != null ? c.Nome : null
                }
            )
            .Where(v => v.Categoria == nome)
            .ToListAsync();

        return Ok(veiculos);
    }


}