using backend_csharp.Interfaces;
using backend_csharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_csharp.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepositorio _repositorio;

    public UsuarioController(IUsuarioRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    [HttpGet]
    public async Task<IActionResult> ListarUsuariosAsync()
    {
        try
        {
            var usuarios = await _repositorio.ListarUsuariosAsync();

            if (!usuarios.Any() || usuarios == null)
            {
                return NotFound("Nenhum usuário foi cadastrado até o momento");
            }

            foreach (var usuario in usuarios)
            {
                usuario.Senha = "*****";
            }

            return Ok(usuarios);
        } catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro inesperado");
        }
    }

    [HttpGet("{id}", Name = "ObterUsuario")]
    public async Task<IActionResult> obterUsuarioAsync(int id)
    {
        try
        {
            var usuario = await _repositorio.obterUsuarioAsync(id);

            if (usuario == null)
            {
                return NotFound("O usuário não foi encontrado!");
            }

            usuario.Senha = "*****";

            return Ok(usuario);
        } catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro inesperado");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarUsuarioAsync([FromBody] Usuario usuario)
    {
        try
        {
            if (usuario == null)
            {
                return BadRequest("Dados inválidos!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idDoUsuario = await _repositorio.AdicionarUsuarioAsync(usuario);
            
            usuario.Id = idDoUsuario;
            usuario.Senha = "*****";

            return CreatedAtRoute("ObterUsuario", new { id = idDoUsuario }, usuario);
        } catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }


}