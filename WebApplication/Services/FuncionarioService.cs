using Microsoft.EntityFrameworkCore;
using WebApplication.DataContext;
using WebApplication.Interfaces;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class FuncionarioService : FuncionarioInterface
    {
        private readonly AppDbContext _context;

        public FuncionarioService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<List<Funcionario>>> Listarfuncionarios()
        {
            Response<List<Funcionario>> response = new Response<List<Funcionario>>();

            try
            {
                var funcionarios = await _context.Funcionarios.ToListAsync();

                if (funcionarios == null || funcionarios.Count == 0)
                {
                    response.Status = false;
                    response.Mensagem = "Nenhum funcionário encontrado.";
                    return response;
                }
                response.Dados = funcionarios;
                response.Mensagem = "Funcionários listados com sucesso.";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
            }
            return response;
        }

        public async Task<Response<Funcionario>> ObterFuncionarioPorId(int id)
        {
            Response<Funcionario> response = new Response<Funcionario>();

            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);

                if (funcionario == null)
                {
                    response.Dados = null;
                    response.Status = false;
                    response.Mensagem = "Funcionário não encontrado.";
                    return response;
                }

                response.Dados = funcionario;
                response.Mensagem = "Funcionário encontrado com sucesso.";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
            }
            return response;
        }

        public async Task<Response<List<Funcionario>>> CriarFuncionario(Funcionario funcionario)
        {
            Response<List<Funcionario>> response = new Response<List<Funcionario>>();

            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(funcionario.Nome) || string.IsNullOrWhiteSpace(funcionario.Sobrenome))
                {
                    response.Status = false;
                    response.Mensagem = "Nome e Sobrenome são obrigatórios.";
                    return response;
                }

                // As datas serão geradas pelo banco automaticamente
                funcionario.DataCriacao = DateTime.UtcNow;
                funcionario.DataAlteracao = DateTime.UtcNow;
                funcionario.Status = true;

                _context.Funcionarios.Add(funcionario);
                await _context.SaveChangesAsync();

                var funcionarios = await _context.Funcionarios.ToListAsync();
                response.Dados = funcionarios;
                response.Mensagem = "Funcionário criado com sucesso.";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro ao criar funcionário: {ex.InnerException?.Message ?? ex.Message}";
            }
            return response;
        }

        public async Task<Response<List<Funcionario>>> AtualizarFuncionario(int id, Funcionario funcionarioAtualizado)
        {
            Response<List<Funcionario>> response = new Response<List<Funcionario>>();

            try
            {
                var funcionarioExistente = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);

                if (funcionarioExistente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Funcionário não encontrado.";
                    return response;
                }

                // Validação
                //if (string.IsNullOrWhiteSpace(funcionario.Nome) || string.IsNullOrWhiteSpace(funcionario.Sobrenome))
                //{
                //     response.Status = false;
                //     response.Mensagem = "Nome e Sobrenome são obrigatórios.";
                //     return response;
                // }

                funcionarioExistente.Nome = funcionarioAtualizado.Nome;
                funcionarioExistente.Sobrenome = funcionarioAtualizado.Sobrenome;
                funcionarioExistente.Departamento = funcionarioAtualizado.Departamento;
                funcionarioExistente.Turno = funcionarioAtualizado.Turno;
                funcionarioExistente.Status = funcionarioAtualizado.Status;
                funcionarioExistente.DataAlteracao = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var funcionarios = await _context.Funcionarios.ToListAsync();
                response.Dados = funcionarios;
                response.Mensagem = "Funcionário atualizado com sucesso.";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro ao atualizar funcionário: {ex.InnerException?.Message ?? ex.Message}";
            }
            return response;
        }

        public async Task<Response<List<Funcionario>>> DeletarFuncionario(int id)
        {
            Response<List<Funcionario>> response = new Response<List<Funcionario>>();
            try
            {
                var funcionarioExistente = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);

                if (funcionarioExistente == null)
                {
                    response.Dados = null;
                    response.Status = false;
                    response.Mensagem = "Funcionário não encontrado.";
                    return response;
                }
                _context.Funcionarios.Remove(funcionarioExistente);
                await _context.SaveChangesAsync();

                var funcionarios = await _context.Funcionarios.ToListAsync();
                response.Dados = funcionarios;
                response.Mensagem = "Funcionário deletado com sucesso.";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Dados = null;
                response.Mensagem = $"Erro ao deletar funcionário: {ex.InnerException?.Message ?? ex.Message}";
                response.Status = false;
            }
            return response;
        }

        public async Task<Response<List<Funcionario>>> InativaFuncionario(int id)
        {
            Response<List<Funcionario>> response = new Response<List<Funcionario>>();

            try
            {
                var funcionarioExistente = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);

                if (funcionarioExistente == null)
                {
                    response.Dados = null;
                    response.Status = false;
                    response.Mensagem = "Funcionário não encontrado.";
                    return response;
                }

                funcionarioExistente.Status = false;
                funcionarioExistente.DataAlteracao = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var funcionarios = await _context.Funcionarios.ToListAsync();
                response.Dados = funcionarios;
                response.Mensagem = "Funcionário inativado com sucesso.";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Dados = null;
                response.Mensagem = $"Erro ao inativar funcionário: {ex.InnerException?.Message ?? ex.Message}";
                response.Status = false;
            }
            return response;
        }
        public async Task<Response<List<Funcionario>>> AtivarFuncionario(int id)
        {
            Response<List<Funcionario>> response = new Response<List<Funcionario>>();
            try
            {
                var funcionarioExistente = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);

                if (funcionarioExistente == null)
                {
                    response.Dados = null;
                    response.Status = false;
                    response.Mensagem = "Funcionário não encontrado.";
                    return response;
                }

                funcionarioExistente.Status = true;
                funcionarioExistente.DataAlteracao = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var funcionarios = await _context.Funcionarios.ToListAsync();
                response.Dados = funcionarios;
                response.Mensagem = "Funcionário ativado com sucesso, lista atualizada.";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Dados = null;
                response.Mensagem = $"Erro ao ativar funcionário: {ex.InnerException?.Message ?? ex.Message}";
                response.Status = false;
            }
            return response;
        }
    }
}
