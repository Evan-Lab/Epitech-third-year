using AreaServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserAreaConfiguration : IEntityTypeConfiguration<UserArea>
{
    string _sqlSchemaName = SvcInfos.sqlSchemaName;

    public void Configure(EntityTypeBuilder<UserArea> builder)
    {

        builder.ToTable(nameof(UserArea), _sqlSchemaName);


        builder.Property(p => p.Name).HasMaxLength(255).IsRequired(true);

        //---

        builder.HasIndex(p => p.StatusArea);
        builder.HasIndex(p => p.StatusInfo);

        //---

        builder.Property(p => p.StatusInfo).HasMaxLength(int.MaxValue).IsRequired(true);

    }
}