using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Design;
using AreaServer.Core.Models;
using AreaServer.Core.Settings;

namespace AreaServer.Infrastructure.Data;

public class AppDbContext : DbContext
{
    IOptions<AppSettings> _appSettings;
    public AppDbContext(IOptions<AppSettings> appSettings, DbContextOptions<AppDbContext> options) : base(options)
    {
        _appSettings = appSettings;
    }
    public DbSet<User> User => Set<User>();

    public DbSet<UserSites> UserSites => Set<UserSites>();

    public DbSet<ActionArea> ActionAreas => Set<ActionArea>();

    public DbSet<ReactionArea> ReactionArea => Set<ReactionArea>();

    public DbSet<UserArea> UserAreas => Set<UserArea>();

    public DbSet<Services> Services => Set<Services>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

