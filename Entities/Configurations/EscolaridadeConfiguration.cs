using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UsuariosEscolaridade.Entities.Configurations;

public class EscolaridadeConfiguration : IEntityTypeConfiguration<EscolaridadeEntity>
{
    public void Configure(EntityTypeBuilder<EscolaridadeEntity> builder)
    {
        builder.HasKey(k => k.Id).HasName("PK__Escolari__3214EC07063D52D9");
        builder.Property(k => k.Id).IsRequired();
    }
}