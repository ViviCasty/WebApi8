using WebApi8.Migrations;

namespace WebApi8.Models
{
    public class ResponseModel<T>
    {
        public T? Dados { get; set; }
        // tipo genérico T e pode ser nulo ?

        public string Mensagem {  get; set; } = string.Empty;

        public bool Status { get; set; } = true;



    }
}
