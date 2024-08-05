using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TesteDevCartsys.Models;

namespace TesteDevCartsys.Data.Dtos;

public class CreatePessoaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Cpf { get; set; }
    public virtual ICollection<TipoPessoa> TiposPessoa { get; set; }

}
