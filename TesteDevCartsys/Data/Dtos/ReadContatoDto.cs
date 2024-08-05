using System.ComponentModel.DataAnnotations;

namespace TesteDevCartsys.Data.Dtos;

public class ReadContatoDto
{
    public string Descricao { get; set; }
    public DateTime HoraConsulta { get; set; } = DateTime.Now;
    public ReadTipoContatoDto TipoContato { get; set; }
}
