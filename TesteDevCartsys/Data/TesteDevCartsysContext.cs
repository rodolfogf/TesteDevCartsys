using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TesteDevCartsys.Data.Dtos;
using TesteDevCartsys.Models;

namespace TesteDevCartsys.Data;

public class TesteDevCartsysContext : DbContext
{
    public TesteDevCartsysContext(DbContextOptions<TesteDevCartsysContext> opts) : base(opts)
    {

    }    

    public DbSet<Contato> Contatos { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<TipoContato> TiposContato { get; set; }
    public DbSet<TipoPessoa> TiposPessoa { get; set; }
}
