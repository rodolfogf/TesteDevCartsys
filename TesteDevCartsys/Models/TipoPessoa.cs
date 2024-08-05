using System.ComponentModel.DataAnnotations;

namespace TesteDevCartsys.Models;

public class TipoPessoa
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "A descrição do tipo de pessoa é obrigatória")]
    public string Descricao { get; set; }
    public ICollection<Pessoa> Pessoas { get; set; }
}
