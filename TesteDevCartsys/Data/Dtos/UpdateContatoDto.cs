using System.ComponentModel.DataAnnotations;

namespace TesteDevCartsys.Data.Dtos;

public class UpdateContatoDto
{

    [StringLength(60, ErrorMessage = "O tamanho máximo para a descrição é de 60 caracteres")]
    public string Descricao { get; set; }
    //public TipoContato TipoContato { get; set; }
}