using Kambam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kambam.Infra.Mappings;

public class CardMap : IEntityTypeConfiguration<CardEntity>
{
    public void Configure(EntityTypeBuilder<CardEntity> builder)
    {
        builder.ToTable("Cards");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Conteudo)
            .IsRequired();

        builder.Property(p => p.Titulo)
            .HasMaxLength(256);

        builder.Property(p => p.Lista)
            .IsRequired()
            .HasMaxLength(256);
    }
}