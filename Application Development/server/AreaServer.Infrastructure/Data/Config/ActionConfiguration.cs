using AreaServer.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
public class ActionConfiguration : IEntityTypeConfiguration<ActionArea>
{
    string _sqlSchemaName = SvcInfos.sqlSchemaName;

    public void Configure(EntityTypeBuilder<ActionArea> builder)
    {
        builder.ToTable(nameof(ActionArea), _sqlSchemaName);

        builder.Property(p => p.Name).HasMaxLength(255).IsRequired(true);

        builder.Property(p => p.Description).HasMaxLength(255).IsRequired(true);

    }
}
