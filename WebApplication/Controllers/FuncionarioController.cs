using Microsoft.AspNetCore.Mvc;
using WebApplication.Interfaces;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioInterface _funcionarioInterface;

        public FuncionarioController(FuncionarioInterface funcionarioInterface)
        {
            _funcionarioInterface = funcionarioInterface;
        }

        [HttpGet]
        public async Task<IActionResult> ListarFuncionarios()
        {
            var response = await _funcionarioInterface.Listarfuncionarios();

            if (response.Status)
            {
                return Ok(response);
            }
            return BadRequest(response.Mensagem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterFuncionarioPorId(int id)
        {
            var response = await _funcionarioInterface.ObterFuncionarioPorId(id);
            if (response.Status)
            {
                return Ok(response);
            }
            return BadRequest(response.Mensagem);
        }

        [HttpPost]
        public async Task<IActionResult> CriarFuncionario(Funcionario funcionario)
        {
            var response = await _funcionarioInterface.CriarFuncionario(funcionario);
            if (response.Status)
            {
                return Ok(response);
            }
            return BadRequest(response.Mensagem);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarFuncionario(int id, Funcionario funcionario)
        {
            var response = await _funcionarioInterface.AtualizarFuncionario(id, funcionario);
            if (response.Status)
            {
                return Ok(response);
            }
            return BadRequest(response.Mensagem);
        }

        [HttpPut("{id}/inativar")]
        public async Task<IActionResult> InativarFuncionario(int id)
        {
            var response = await _funcionarioInterface.InativaFuncionario(id);
            if (response.Status)
            {
                return Ok(response);
            }
            return BadRequest(response.Mensagem);
        }

        [HttpPut("{id}/ativar")]
        public async Task<IActionResult> AtivarFuncionario(int id)
        {
            var response = await _funcionarioInterface.AtivarFuncionario(id);

            if (response.Status)
            {
                return Ok(response);
            }
            return BadRequest(response.Mensagem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarFuncionario(int id)
        {
            var response = await _funcionarioInterface.DeletarFuncionario(id);
            if (response.Status)
            {
                return Ok(response);
            }
            return BadRequest(response.Mensagem);
        }
    }
}
