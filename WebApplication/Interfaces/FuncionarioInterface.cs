using WebApplication.Models;

namespace WebApplication.Interfaces
{
    public interface FuncionarioInterface
    {
        Task<Response<List<Funcionario>>> Listarfuncionarios(); 
        Task<Response<Funcionario>> ObterFuncionarioPorId(int id);
        Task<Response<List<Funcionario>>> CriarFuncionario(Funcionario funcionario);
        Task<Response<List<Funcionario>>> AtualizarFuncionario(int id, Funcionario funcionarioAtualizado);
        Task<Response<List<Funcionario>>> DeletarFuncionario(int id);
        Task<Response<List<Funcionario>>> InativaFuncionario(int id);
        Task<Response<List<Funcionario>>> AtivarFuncionario(int id);
    }
}
