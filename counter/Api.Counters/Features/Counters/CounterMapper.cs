using Api.Counters.Database.Models;
using AutoMapper;

namespace Api.Counters.Features.Counters;

public class CounterMapper : Profile
{
    public CounterMapper()
    {
        CreateMap<Counter, CounterDto>();

        CreateMap<IncrementCounter.Command, Counter>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CreateCounter.Command, Counter>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
