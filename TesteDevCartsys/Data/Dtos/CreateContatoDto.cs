using System.ComponentModel.DataAnnotations;

namespace TesteDevCartsys.Data.Dtos;

public class CreateContatoDto
{
    //[Required(ErrorMessage = "A descriação do contato é obrigatória")]
    //[StringLength(60, ErrorMessage = "O tamanho máximo para a descrição é de 60 caracteres")]
    public string Descricao { get; set; }
    //[Required(ErrorMessage = "O id do tipo de contato é obrigatório")]
    public int TipoContatoId { get; set; }
}
