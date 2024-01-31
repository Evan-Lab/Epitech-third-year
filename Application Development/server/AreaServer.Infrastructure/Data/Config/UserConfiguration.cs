using AreaServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    string _sqlSchemaName = SvcInfos.sqlSchemaName;

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User), _sqlSchemaName);

        builder.Property(p => p.Firstname).HasMaxLength(255).IsRequired(true);

        builder.Property(p => p.Lastname).HasMaxLength(255).IsRequired(true);

        builder.Property(p => p.Email).HasMaxLength(255).IsRequired(true);

        builder.Property(p => p.Password).HasMaxLength(255).IsRequired(true);

        builder.Property(p => p.PhotoUrl).HasMaxLength(255).IsRequired(false);

        //---

        builder.HasIndex(p => p.Email).IsUnique();

        builder.HasIndex(p => p.AccountStatus);

        builder.HasIndex(p => p.Admin);

        //---

    }
}
