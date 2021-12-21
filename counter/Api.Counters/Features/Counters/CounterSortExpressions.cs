using System.Linq.Expressions;
using Api.Counters.Database.Models;
using Humanizer;

namespace Api.Counters.Features.Counters;

public static class CounterSortExpressions
{
    public static Expression<Func<Counter, object>> Get(string propertyName)
    {
        return propertyName?.Pascalize() switch
        {
            nameof(CounterDto.Id) => counter => counter.Id,
            nameof(CounterDto.Name) => counter => counter.Name,
            nameof(CounterDto.Value) => counter => counter.Value,
            _ => counter => counter.Id
        };
    }
}
