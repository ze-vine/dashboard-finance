using backend_csharp.Interfaces;
using backend_csharp.Models;
using Microsoft.AspNetCore.Mvc; 

namespace backend_csharp.Controllers;

[ApiController]
[Route("api/categorias")]
public class CategoriaController : ControllerBase
{
  
  private readonly ICategoriaRepositorio _repositorio;
  
  public CategoriaController(ICategoriaRepositorio repositorio)
  {
    _repositorio = repositorio;
  }
  
  [HttpGet("{idUsuario}")]
  public async Task<IActionResult> ListarCategoriasDoUsuario(int idUsuario)
  {
    try
    {
      var categorias = await _repositorio.ListarCategoriasDoUsuario(idUsuario);
      
      if (categorias == null || !categorias.Any())
      {
        return NotFound("Nenhuma categoria foi cadastrada até o momento para esse usuário!");
      }
      
      return Ok(categorias);
    } catch (Exception e)
    {
      return StatusCode(500, "Ocorreu um erro inesperado!");
    }
    
  }
  
}