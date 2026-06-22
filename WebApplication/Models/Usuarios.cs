using WebApplication.Enums;

namespace WebApplication.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }
        public Cargos Cargo { get; set; }
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }
        public DateTime TokenDataCriacao { get; set; } = DateTime.UtcNow;

    }
}
