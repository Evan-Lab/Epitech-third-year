using AreaServer.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class ReactionConfiguration : IEntityTypeConfiguration<ReactionArea>
{
    string _sqlSchemaName = SvcInfos.sqlSchemaName;

    public void Configure(EntityTypeBuilder<ReactionArea> builder)
    {
        builder.ToTable(nameof(ReactionArea), _sqlSchemaName);

        builder.Property(p => p.Name).HasMaxLength(255).IsRequired(true);

        builder.Property(p => p.Description).HasMaxLength(255).IsRequired(true);

    }
}
