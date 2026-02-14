using backend_csharp.Interfaces;
using backend_csharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepositorio repositorio;

    public UsuarioController(IUsuarioRepositorio repositorio)
    {
        this.repositorio = repositorio;
    }

    public async Task<IActionResult> listarUsuarios()
    {
        var usuarios = await repositorio.ListarTodos();
        return Ok(usuarios);
    }
}