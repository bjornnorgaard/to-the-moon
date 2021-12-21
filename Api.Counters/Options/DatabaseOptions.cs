using Ant.Platform.Options;

namespace Api.Counters.Options;

public class DatabaseOptions : AbstractOptions
{
    public string CounterDatabase { get; set; }

    public DatabaseOptions(IConfiguration configuration) : base(configuration)
    {
    }
}
