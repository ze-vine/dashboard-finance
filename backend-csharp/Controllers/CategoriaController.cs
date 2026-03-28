using backend_csharp.DTO;
using backend_csharp.Interfaces;
using Microsoft.AspNetCore.Mvc; 

namespace backend_csharp.Controllers;

[ApiController]
[Route("api/categorias")]
public class CategoriaController : ControllerBase
{
  
  private readonly ICategoriaRepositorio _repositorio;

  /* Essa variável simula o id de um usuário do sistema para fins de teste. Posteriormente,
  implementarei com o JWT.
  */
  private int idMock = 1;
  
  public CategoriaController(ICategoriaRepositorio repositorio)
  {
    _repositorio = repositorio;
  }
  
  [HttpGet]
  public async Task<IActionResult> ListarCategoriasDoUsuario()
  {
    try
    {
      var categoriasModel = await _repositorio.ListarCategoriasDoUsuario(idMock);
      
      if (categoriasModel == null || !categoriasModel.Any())
      {
        return Ok(new { 
          dados = Array.Empty<CategoriaListagemDTO>(), 
          mensagem = "Nenhuma categoria foi cadastrada para esse usuário!"
        });
      }
      
      var categoriasDTO = categoriasModel.Select(c => new CategoriaListagemDTO
      {
        Id = c.Id,
        Nome = c.Nome,
        Ativo = c.Ativo
      });

      return Ok(new { 
        dados = categoriasDTO, 
        mensagem = "Categorias retornadas com sucesso!"
      });

    } catch (Exception e)
    {
      Console.WriteLine(e.Message);
      return StatusCode(500, "Ocorreu um erro inesperado!");
    }
    
  }
  
}