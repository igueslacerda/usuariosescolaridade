using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UsuariosEscolaridade.Entities.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<UsuarioEntity>
{
    public void Configure(EntityTypeBuilder<UsuarioEntity> builder)
    {
        builder.HasKey(k => k.Id).HasName("PK__Usuarios__3214EC07FED0802B");
        builder.HasOne(u => u.Escolaridade).WithMany(ec => ec.Usuarios)
                .HasForeignKey(u => u.EscolaridadeId)
                .HasConstraintName("FK__Usuarios__Escola__286302EC");
    }
}