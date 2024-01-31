using AreaServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserSitesConfiguration : IEntityTypeConfiguration<UserSites>
{
    string _sqlSchemaName = SvcInfos.sqlSchemaName;

    public void Configure(EntityTypeBuilder<UserSites> builder)
    {

        builder.ToTable(nameof(UserSites), _sqlSchemaName);


        builder.Property(p  => p.Name).HasMaxLength(255).IsRequired(true);

        builder.Property(p => p.Email).HasMaxLength(255).IsRequired(true);

        //---

    }
}