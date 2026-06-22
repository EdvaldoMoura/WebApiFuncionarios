namespace WebApi.Models
{
    public class Response<Any>
    {
        public Any? Dados { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
}
