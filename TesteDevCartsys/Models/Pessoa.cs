using System.ComponentModel.DataAnnotations;

namespace TesteDevCartsys.Models;

public class Pessoa
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MaxLength(60)]
    public string Nome { get; set; }
    [Required(ErrorMessage = "A data de nascimento do paciente é obrigatória")]
    [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
    public DateTime DataNascimento { get; set; }
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Formato inválido de CPF")]
    public string Cpf { get; set; }
    public virtual ICollection<Contato> Contatos { get; set; }
}
