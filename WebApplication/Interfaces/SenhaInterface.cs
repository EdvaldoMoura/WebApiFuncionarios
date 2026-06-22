using WebApplication.Models;

namespace WebApplication.Interfaces
{
    public interface SenhaInterface
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificarSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt);
        string GerarToken(Usuarios usuario);

    }
}
