namespace WebApi.Middleware
{
    public class ApiException
    {
        public ApiException(string statusCode, string mensagem, string details)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
            Details = details;
        }
        public string StatusCode { get; set; }
        public string Mensagem { get; set; }
        public string Details { get; set; }
    }
}
