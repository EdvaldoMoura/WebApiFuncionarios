using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }
        
        [HttpGet]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios();
            if (!usuarios.Status)
            {
                return NotFound(usuarios);
            }
            return Ok(usuarios);
        }
        [HttpGet("BuscarUsuariosPorId/{idUsuario}")]
        public async Task<IActionResult> BuscarUsuariosPorId(int idUsuario)
        {
            var usuarios = await _usuarioInterface.BuscarUsuariosPorId(idUsuario);

            if (usuarios == null)
            {
                return NotFound(usuarios);
            }
            return Ok(usuarios);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarUsuarioPorId(int id)
        {
            var usuarios = await _usuarioInterface.DeletarUsuarioPorId(id);
            if (usuarios == null)
            {
                return NotFound(usuarios);
            }
            return Ok(usuarios);
        }
        [HttpPost]
        public async Task<IActionResult> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            var usuarioCriado = await _usuarioInterface.CriarUsuario(usuarioCriarDto);
            if (usuarioCriado == null)
            {
                return BadRequest(usuarioCriado);
            }
            return Ok(usuarioCriado);
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            var usuarioEditado = await _usuarioInterface.EditarUsuario(usuarioEditarDto);
            if (usuarioEditado == null)
            {
                return BadRequest(usuarioEditado);
            }
            return Ok(usuarioEditado);
        }
    }
}
