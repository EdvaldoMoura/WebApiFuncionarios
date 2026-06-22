namespace WebApi.Dto
{
    public class UsuarioEditarDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public double Salario { get; set; }
        public string Cpf { get; set; }
        public bool Situacao { get; set; } // 1 = ativo e 0 = inativo
        
    }
}
