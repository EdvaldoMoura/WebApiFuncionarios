using AutoMapper;
using Dapper;
using Npgsql;
using WebApi.Dto;
using WebApi.Models;


namespace WebApi.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsuarioService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<Response<List<UsuarioListarDto>>> BuscarUsuarios()
        {
            Response<List<UsuarioListarDto>> response = new Response<List<UsuarioListarDto>>();

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))

            {
                var usuarioBanco = await connection.QueryAsync<Usuario>("SELECT * FROM usuarios");

                if (usuarioBanco.Count() == 0) {

                    response.Mensagem = "Nenhum usuário encontrado.";
                    response.Status = false;
                    return response;
                }

                var usuarioListarDto = _mapper.Map<List<UsuarioListarDto>>(usuarioBanco);
                response.Dados = usuarioListarDto;
                response.Mensagem = "Usuários encontrados com sucesso.";
                response.Status = true;
            }
            return response;
        }

        public async Task<Response<UsuarioListarDto>> BuscarUsuariosPorId(int idUsuario)
        {
            Response<UsuarioListarDto> response = new Response<UsuarioListarDto>();

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))

            {
                var usuarioBanco = await connection.QueryFirstOrDefaultAsync<Usuario>(
                    "SELECT * FROM Usuarios WHERE Id = @Id", new { Id = idUsuario });

                if (usuarioBanco == null)
                {

                    response.Mensagem = "Nenhum usuário encontrado.";
                    response.Status = false;
                    return response;
                }

                var usuarioListarDto = _mapper.Map<UsuarioListarDto>(usuarioBanco);
                response.Dados = usuarioListarDto;
                response.Mensagem = "Usuários encontrados com sucesso.";
                response.Status = true;
            }
            return response;
        }

        public async Task<Response<List<UsuarioListarDto>>> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            Response<List<UsuarioListarDto>> response = new Response<List<UsuarioListarDto>>();

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                // 1. Verifica se o e-mail já está cadastrado
                var sqlCheckEmail = "SELECT COUNT(1) FROM Usuarios WHERE Email = @Email";
                var emailExiste = await connection.ExecuteScalarAsync<bool>(sqlCheckEmail, new { Email = usuarioCriarDto.Email });

                if (emailExiste)
                {
                    response.Mensagem = "Este e-mail já está cadastrado no sistema.";
                    response.Status = false;
                    return response; // Retorna aqui e impede a inserção
                }

                // 1. Comando de inserção utilizando os dados do DTO
                var sqlInsert = @"INSERT INTO Usuarios (Nome, Email, Cargo, Salario, Cpf, Situacao, Senha) 
                          VALUES (@Nome, @Email, @Cargo, @Salario, @Cpf, @Situacao, @Senha)";

                await connection.ExecuteAsync(sqlInsert, usuarioCriarDto);

                // 2. Busca a lista completa de usuários para satisfazer o retorno List<UsuarioListarDto>
                var usuariosBanco = await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");

                // 3. Mapeia a lista de modelos para a lista de DTOs de listagem
                response.Dados = _mapper.Map<List<UsuarioListarDto>>(usuariosBanco.ToList());
                response.Mensagem = "Usuário cadastrado com sucesso!";
                response.Status = true;
            }

            return response;
        }

        public async Task<Response<List<UsuarioListarDto>>> DeletarUsuarioPorId(int id)
        {
            Response<List<UsuarioListarDto>> response = new Response<List<UsuarioListarDto>>();

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                // 1. Executa o comando de delete
                var usuarioDeletar = await connection.ExecuteAsync(
                    "DELETE FROM Usuarios WHERE Id = @Id", new { Id = id });

                // 2. Verifica se algum registro foi realmente removido
                if (usuarioDeletar == 0)
                {
                    response.Mensagem = "Nenhum usuário encontrado para exclusão.";
                    response.Status = false;
                    return response;
                }

                // 3. Opcional: Busca a lista atualizada para retornar ao Front-end
                var usuariosRestantes = await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");

                response.Dados = _mapper.Map<List<UsuarioListarDto>>(usuariosRestantes);
                response.Mensagem = "Usuário deletado com sucesso!";
                response.Status = true;
            }

            return response;
        }

        public async Task<Response<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            Response<List<UsuarioListarDto>> response = new Response<List<UsuarioListarDto>>();

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                // 1. Comando de atualização utilizando os dados do DTO
                var sqlUpdate = @"UPDATE Usuarios 
                                  SET Nome = @Nome, Email = @Email, Cargo = @Cargo, Salario = @Salario, Cpf = @Cpf, Situacao = @Situacao 
                                  WHERE Id = @Id";

                var rowsAffected = await connection.ExecuteAsync(sqlUpdate, usuarioEditarDto);

                // 2. Verifica se a atualização foi bem-sucedida
                if (rowsAffected == 0)
                {
                    response.Mensagem = "Nenhum usuário encontrado para atualização.";
                    response.Status = false;
                    return response;
                }
                // 3. Opcional: Busca a lista atualizada para retornar ao Front-end
                var usuariosAtualizados = await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");
                response.Dados = _mapper.Map<List<UsuarioListarDto>>(usuariosAtualizados);
                response.Mensagem = "Usuário atualizado com sucesso!";
                response.Status = true;
            }
            return response;
        }
    }
}
