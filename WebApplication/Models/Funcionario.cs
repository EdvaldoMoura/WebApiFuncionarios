using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication.Enums;

namespace WebApplication.Models
{
    [Table("funcionarios")]
    public class Funcionario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Column("sobrenome")]
        [Required(ErrorMessage = "Sobrenome é obrigatório")]
        [StringLength(100)]
        public string Sobrenome { get; set; } = string.Empty;

        [Column("departamento")]
        public Departamentos Departamento { get; set; }

        [Column("turno")]
        public Turnos Turno { get; set; }

        [Column("status")]
        public bool Status { get; set; } = true;

        [Column("data_criacao")]
        public DateTime DataCriacao { get; set; }

        [Column("data_alteracao")]
        public DateTime DataAlteracao { get; set; }
    }
}
