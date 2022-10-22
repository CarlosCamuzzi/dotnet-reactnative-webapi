using System.Text.Json;
using CadastroPessoa.Data;
using CadastroPessoa.Models;
using CadastroPessoa.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroPessoa.Controllers
{
  [ApiController]
  [Route(template: "v1")]
  public class PessoaControllers : ControllerBase
  {
    [HttpGet(template: "pessoas/{id}")]
    public async Task<IActionResult> GetPessoaAsync(
        [FromServices] AppDbContext context,
        [FromRoute] int id)
    {
      var pessoa = await context.Pessoas
      .AsNoTracking()
      .FirstOrDefaultAsync(x => x.Id == id);

      string jsonString = JsonSerializer.Serialize(pessoa);

      return pessoa == null ? NotFound() : Ok(jsonString);
    }

    [HttpPost(template: "pessoas")]
    public async Task<IActionResult> PostPessoaAsync(
        [FromServices] AppDbContext context,
        [FromBody] CreatePessoaViewModel model)
    {
      if (!ModelState.IsValid) return BadRequest();

      var pessoa = new Pessoa()
      {
        Nome = model.Nome,
        Idade = model.Idade,
        Curso = model.Curso
      };     

      try
      {
        await context.AddAsync(pessoa);
        await context.SaveChangesAsync();
        return Created($"v1/pessoas;{pessoa.Id}", pessoa);
      }
      catch (System.Exception)
      {
        return BadRequest();
      }
    }

    [HttpPut(template: "pessoas/{id}")]
    public async Task<IActionResult> PutPessoaAsync(
        [FromServices] AppDbContext context,
        [FromBody] CreatePessoaViewModel model,
        [FromRoute] int id)
    {
      if (!ModelState.IsValid) return BadRequest();

      var pessoa = await context.Pessoas
      .FirstOrDefaultAsync(x => x.Id == id);

      if (pessoa == null) return NotFound();

      try
      {
        pessoa.Nome = model.Nome;
        pessoa.Idade = model.Idade;
        pessoa.Curso = model.Curso;

        context.Pessoas.Update(pessoa);
        await context.SaveChangesAsync();

        return Ok(pessoa);
      }
      catch (System.Exception)
      {
        return BadRequest();
      }
    }

    [HttpDelete(template: "pessoas/{id}")]
    public async Task<IActionResult> DeltePessoaAsync(
        [FromServices] AppDbContext context,
        [FromRoute] int id)
    {
      var pessoa = await context.Pessoas
      .FirstOrDefaultAsync(x => x.Id == id);

      try
      {
        context.Pessoas.Remove(pessoa);
        await context.SaveChangesAsync();

        return Ok("Excluído");
      }
      catch (System.Exception)
      {
        return BadRequest();
      }
    }
  }
}