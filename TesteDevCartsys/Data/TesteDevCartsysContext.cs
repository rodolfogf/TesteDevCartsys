using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TesteDevCartsys.Models;

namespace TesteDevCartsys.Data;

public class TesteDevCartsysContext : IdentityDbContext<Usuario>
{
    public TesteDevCartsysContext(DbContextOptions<TesteDevCartsysContext> opts) : base(opts)
    {

    }

    public DbSet<Contato> Contatos { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<TipoContato> TiposContato { get; set; }
    public DbSet<TipoPessoa> TiposPessoa { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Pessoa>()
            .HasMany(p => p.Contatos)
            .WithOne(c => c.Pessoa);
            
        builder.Entity<Contato>()
            .HasOne(c => c.TipoPessoa)
            .WithMany()
            .HasForeignKey(c => c.TipoPessoaId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<Contato>()
            .HasOne(c => c.TipoContato)
            .WithMany()
            .HasForeignKey(c => c.TipoContatoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Contato>()
            .HasOne(c => c.Pessoa)
            .WithMany(p => p.Contatos)
            .HasForeignKey(c => c.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<IdentityUserRole<string>>()
        .HasNoKey();

        builder.Entity<IdentityUserLogin<string>>()
        .HasKey(login => new { login.LoginProvider, login.ProviderKey });

        builder.Entity<IdentityUserToken<string>>()
        .HasKey(token => new { token.UserId, token.LoginProvider, token.Name });
    }
        
}
