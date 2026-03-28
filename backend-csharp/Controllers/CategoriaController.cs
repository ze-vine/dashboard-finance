using backend_csharp.DTO;
using backend_csharp.Interfaces;
using backend_csharp.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

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

  [HttpGet("{id}")]
  public async Task<IActionResult> BuscarUmaCategoria(int id)
  {
    try
    {
      var categoria = await _repositorio.BuscarUmaCategoria(idMock, id);

      if (categoria == null)
      {
        return NotFound(new { mensagem = "Esta categoria não foi encontrada!" });
      }

      return Ok(new CategoriaListagemDTO
      {
        Id = categoria.Id,
        Nome = categoria.Nome,
        Ativo = categoria.Ativo
      }); 
    } 
    catch (InvalidOperationException e)
    {
      Console.WriteLine(e.Message);
      return NotFound(new { mensagem = "Nenhuma categoria foi encontrada!" });
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
      return StatusCode(500, "Ocorreu um erro inesperado!");
    }
  }

  [HttpPost]
  public async Task<IActionResult> CriarUmaCategoria(CategoriaCriacaoDTO categoriaDTO)
  {
    try
    {

      Categoria categoriaModel = new Categoria
      {
        Nome = categoriaDTO.Nome,
        Ativo = true,
        IdUsuario = idMock
      };

      var novaCategoria = await _repositorio.CriarUmaCategoria(categoriaModel);
/*
      return Ok(new CategoriaResponseCriacaoDTO
      {
        Id = novaCategoria.Id,
        Nome = novaCategoria.Nome
      });
*/
      CategoriaListagemDTO categoriaDeListagem = new CategoriaListagemDTO
      {
        Id = novaCategoria.Id,
        Nome = novaCategoria.Nome,
        Ativo = novaCategoria.Ativo
      };

      return Created($"/api/categorias/{categoriaDeListagem.Id}", categoriaDeListagem);

    } catch (MySqlException e) when (e.Number == 1062)
    {

      return Conflict(new { mensagem = "Erro! Já existe uma categoria com o nome informado!" });

    } catch(Exception e)
    {

      Console.WriteLine(e.Message);
      return StatusCode(500, "Ocorreu um erro inesperado!");

    }
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> AtualizarUmaCategoria(int id, CategoriaAtualizacaoDTO categoriaDTO)
  {
    try
    {
      var categoria = await _repositorio.BuscarUmaCategoria(idMock, id);

      if (categoria == null)
      {
        return NotFound(new { mensagem = "A categoria não foi encontrada!" });
      }

      if (!categoria.Ativo)
      {
        return BadRequest(new { mensagem = "Esta categoria precisa estar ativa para atualização!" });
      }

      categoria.Nome = categoriaDTO.Nome;
      categoria.IdUsuario = idMock;

      if (!await _repositorio.AtualizarUmaCategoria(categoria))
      {
        return BadRequest(new { mensagem = "Não foi possível atualizar esta categoria!"});
      }

      CategoriaListagemDTO categoriaResponse = new CategoriaListagemDTO
      {
        Id = categoria.Id,
        Nome = categoria.Nome,
        Ativo = categoria.Ativo
      };

      return Ok(new
      {
        dados = categoriaResponse,
        mensagem = "Categoria atualizada com sucesso!"
      });
    } 
    catch (MySqlException e) when (e.Number == 1062)
    {
      return Conflict(new { mensagem = "Erro! Já existe uma categoria com o nome informado!" });
    }
    catch (InvalidOperationException e)
    {
      return NotFound(new { mensagem = "Não foi possível atualizar, pois esta categoria não foi encontrada!" });
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
      return StatusCode(500, "Ocorreu um erro inesperado!");
    }
  }
  
}