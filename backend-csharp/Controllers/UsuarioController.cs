using backend_csharp.Interfaces;
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
    public async Task<IActionResult> ListarUsuarios()
    {
        try
        {
            var usuarios = await _repositorio.ListarUsuarios();

            if (!usuarios.Any())
            {
                return NotFound("Nenhum usuário foi cadastrado até o momento");
            }

            return Ok(usuarios);

        } catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro inesperado");
        }
    }
}