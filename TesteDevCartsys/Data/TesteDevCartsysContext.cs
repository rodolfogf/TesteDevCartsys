using Microsoft.EntityFrameworkCore;
using TesteDevCartsys.Models;

namespace TesteDevCartsys.Data;

public class TesteDevCartsysContext : DbContext
{
    public TesteDevCartsysContext(DbContextOptions<TesteDevCartsysContext> opts) : base(opts)
    {

    }
    public DbSet<Contato> Contatos { get; set; }
}
