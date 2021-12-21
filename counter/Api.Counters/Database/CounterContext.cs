using Api.Counters.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Counters.Database;

public class CounterContext : DbContext
{
    public DbSet<Counter> Counters { get; set; }

    public CounterContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CounterContext).Assembly);
    }
}
