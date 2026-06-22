using System.ComponentModel.DataAnnotations;
using WebApplication.Enums;

namespace WebApplication.DTOs
{
    public class UsuarioCriacaoDto
    {
        [Required(ErrorMessage = "O campo 'Usuario' é obrigatório.")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "O campo 'Email' é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo 'Senha' é obrigatório.")]
        public string Senha { get; set; }
        [Compare("Senha", ErrorMessage = "Senhas não são iguais")]
        public string ConfirmaSenha { get; set; }
        [Required(ErrorMessage = "O campo 'Cargo' é obrigatório.")]
        public Cargos Cargo { get; set; }
    }
}
