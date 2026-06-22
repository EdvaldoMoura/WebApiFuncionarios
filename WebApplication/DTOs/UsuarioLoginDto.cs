using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTOs
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "O campo Email é obrigatório."), EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")] 
        public string Senha { get; set; }



    }
}
