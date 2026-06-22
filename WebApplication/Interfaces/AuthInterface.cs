using WebApplication.DTOs;
using WebApplication.Models;

namespace WebApplication.Interfaces
{
    public interface AuthInterface
    {
        Task<Response<UsuarioCriacaoDto>> Registrar(UsuarioCriacaoDto usuarioCriacaoDto);
        Task<Response<string>> Login(UsuarioLoginDto usuarioLoginDto);
        Task<Response<List<UsuariosDto>>> GetAllUsers();
        

        

    }
}
