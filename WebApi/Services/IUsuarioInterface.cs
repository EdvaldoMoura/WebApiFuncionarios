using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IUsuarioInterface
    {
        Task<Response<List<UsuarioListarDto>>> BuscarUsuarios();
        Task<Response<UsuarioListarDto>> BuscarUsuariosPorId(int idUsuario);
        Task<Response<List<UsuarioListarDto>>> DeletarUsuarioPorId(int id);
        Task<Response<List<UsuarioListarDto>>> CriarUsuario(UsuarioCriarDto usuarioCriarDto);
        Task<Response<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto);



    }
}
