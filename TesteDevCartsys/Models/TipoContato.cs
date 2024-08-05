using System.ComponentModel.DataAnnotations;

namespace TesteDevCartsys.Models
{
    public class TipoContato
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "A descrição do tipo de contato é obrigatória")]
        public string Descricao { get; set; }
        public virtual Contato Contato { get; set; }
    }
}