using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DTOs;
using WebApplication.Interfaces;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthInterface _authInterface;
        public AuthController(AuthInterface authInterface)
        {
            _authInterface = authInterface;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLogin)
        {
            var response = await _authInterface.Login(usuarioLogin);
            if (response.Status)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UsuarioCriacaoDto usuarioRegister)
        {
            var response = await _authInterface.Registrar(usuarioRegister);
            if (response.Status)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _authInterface.GetAllUsers();
            if (response.Status)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
