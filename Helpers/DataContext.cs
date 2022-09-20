namespace UsuariosEscolaridade.Helpers;

using Microsoft.EntityFrameworkCore;
using UsuariosEscolaridade.Entities;
using UsuariosEscolaridade.Entities.Configurations;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DbSet<EscolaridadeEntity> Escolaridade { get; set; }
    public DbSet<UsuarioEntity> Usuarios { get; set; }

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // conecta em sql server utilizando connection string do app settings
        options.UseSqlServer(Configuration.GetConnectionString("IguesSomeeSQL"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EscolaridadeConfiguration());
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}