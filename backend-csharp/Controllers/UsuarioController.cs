using backend_csharp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend_csharp.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepositorio repositorio;

    public UsuarioController(IUsuarioRepositorio repositorio)
    {
        this.repositorio = repositorio;
    }

    [HttpGet]
    public async Task<IActionResult> listarUsuarios()
    {
        var usuarios = await repositorio.ListarTodos();
        return Ok(usuarios);
    }
}