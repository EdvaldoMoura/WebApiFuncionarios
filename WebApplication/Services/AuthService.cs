using Microsoft.EntityFrameworkCore;
using WebApplication.DataContext;
using WebApplication.DTOs;
using WebApplication.Interfaces;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class AuthService : AuthInterface
    {
        private readonly AppDbContext _context;
        private readonly SenhaInterface _senha;
        public AuthService(AppDbContext context, SenhaInterface senha)
        {
            _context = context;
            _senha = senha;
        }

        public Task<Response<List<UsuariosDto>>> GetAllUsers()
        {
           Response<List<UsuariosDto>> response = new Response<List<UsuariosDto>>();
            try
            {
                var usuarios = _context.Usuarios.Select(u => new UsuariosDto
                {
                    Email = u.Email,
                    Usuario = u.Usuario,
                    Cargo = u.Cargo
                }).ToList();

                response.Dados = usuarios;
                response.Mensagem = "Usuários recuperados com sucesso.";
                response.Status = true;
                return Task.FromResult(response);
            }
            catch (Exception erro)
            {
                response.Dados = null;
                response.Mensagem = $"Ocorreu um erro ao recuperar os usuários: {erro.Message}";
                response.Status = false;
                return Task.FromResult(response);
            }
        }

        public async Task<Response<string>> Login(UsuarioLoginDto usuarioLoginDto)
        {
           Response<string> response = new Response<string>();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuarioLoginDto.Email);

                if (usuario == null)
                {
                    response.Dados = null;
                    response.Mensagem = "Email ou senha inválidos.";
                    response.Status = false;
                    return response;
                }
                if (!_senha.VerificarSenhaHash(usuarioLoginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    response.Dados = null;
                    response.Mensagem = "Email ou senha inválidos.";
                    response.Status = false;
                    return response;
                }
                //Gera token de autenticação aqui (JWT, por exemplo) e adicione ao response.Dados se necessário
                var token =_senha.GerarToken(usuario);
                response.Mensagem = "Login bem-sucedido.";

                response.Dados = token;
                response.Mensagem = "Login bem-sucedido.";
                response.Status = true;
                return response;
            }
            catch (Exception erro)
            {
                response.Dados = null;
                response.Mensagem = $"Ocorreu um erro ao realizar o login: {erro.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<Response<UsuarioCriacaoDto>> Registrar(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            Response<UsuarioCriacaoDto> response = new Response<UsuarioCriacaoDto>();

            try
            {
                if (!VerificarEmailUsuarioExistente(usuarioCriacaoDto))
                {
                    response.Dados = null;
                    response.Mensagem = "O email ou usuário já está em uso.";
                    response.Status = false;
                    return response;
                }
                
                byte[] senhaHash, senhaSalt;
                _senha.CriarSenhaHash(usuarioCriacaoDto.Senha, out senhaHash, out senhaSalt);

                Usuarios usuario = new Usuarios
                {
                    Usuario = usuarioCriacaoDto.Usuario,
                    Email = usuarioCriacaoDto.Email,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt,
                    Cargo = usuarioCriacaoDto.Cargo
                };
                
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Usuarios
                    .Where(u => u.Email == usuarioCriacaoDto.Email)
                    .Select(u => new UsuarioCriacaoDto
                    {
                        Usuario = u.Usuario,
                        Email = u.Email,
                        Cargo = u.Cargo
                    })
                    .FirstOrDefaultAsync();
                response.Mensagem = "Usuário registrado com sucesso.";
                response.Status = true;
                return response;



            }
            catch (Exception erro)
            {

                response.Dados = null;
                response.Mensagem = $"Ocorreu um erro ao registar o usuário: {erro.Message}";
                response.Status = false;
                return response;
            }
        }
        public bool VerificarEmailUsuarioExistente(UsuarioCriacaoDto usuarioRegistro)
        {
            var usuario =  _context.Usuarios.FirstOrDefault(u => u.Email == usuarioRegistro.Email || u.Usuario == usuarioRegistro.Usuario);
            if (usuario != null)
            {
                return false;
            }
            return true;
        }
    }
}
