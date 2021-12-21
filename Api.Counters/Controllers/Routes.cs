namespace Api.Counters.Controllers;

public static class Routes
{
    private const string Api = "";

    public static class Counters
    {
        public const string Base = $"{Api}counters";
        public const string GetCounter = $"{Base}/get-counter";
        public const string GetCounters = $"{Base}/get-counters";
        public const string CreateCounter = $"{Base}/create-counter";
        public const string IncrementCounter = $"{Base}/increment-counter";
        public const string DecrementCounter = $"{Base}/decrement-counter";
        public const string DeleteCounter = $"{Base}/delete-counter";
    }
}
