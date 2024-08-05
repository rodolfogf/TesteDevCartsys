using System.ComponentModel.DataAnnotations;

namespace TesteDevCartsys.Controllers.Dtos
{
    public class ReadContatoDto
    {
        public string Descricao { get; set; }
        public DateTime HoraConsulta { get; set; } = DateTime.Now;
        //public TipoContato TipoContato { get; set; }
    }
}
