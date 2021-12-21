namespace Api.Counters.Database.Models;

public class Counter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
}
