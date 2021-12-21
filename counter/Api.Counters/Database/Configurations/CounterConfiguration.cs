using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Counters.Database.Configurations;

public class CounterConfiguration : IEntityTypeConfiguration<Models.Counter>
{
    public void Configure(EntityTypeBuilder<Models.Counter> todo)
    {
        todo.HasKey(t => t.Id);
        todo.Property(t => t.Name).HasMaxLength(CounterConstants.Name.MaxLength).IsRequired();
        todo.Property(t => t.Value);
    }
}
