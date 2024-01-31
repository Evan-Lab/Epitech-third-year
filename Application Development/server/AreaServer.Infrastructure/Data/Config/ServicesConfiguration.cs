using AreaServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class SvcInfos
{
    public const string sqlSchemaName = "schm_areaservices";
}

public class ServicesConfiguration : IEntityTypeConfiguration<Services>
{
    string _sqlSchemaName = SvcInfos.sqlSchemaName;

    public void Configure(EntityTypeBuilder<Services> builder)
    {
        builder.ToTable(nameof(Services), _sqlSchemaName);

        builder.Property(p => p.Name).HasMaxLength(255).IsRequired(true);

        //---

        //---

        builder.Property(p => p.LogoUrl).HasMaxLength(550).IsRequired(false);

    }
}
