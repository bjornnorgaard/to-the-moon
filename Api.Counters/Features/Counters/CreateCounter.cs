using Api.Counters.Database;
using Api.Counters.Database.Configurations;
using Api.Counters.Database.Models;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Api.Counters.Features.Counters;

public class CreateCounter
{
    public class Command : IRequest<Result>
    {
        public string Name { get; set; }
    }

    public class Result
    {
        public CounterDto CreatedCounter { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .MinimumLength(CounterConstants.Name.MinLength)
                .MaximumLength(CounterConstants.Name.MaxLength);
        }
    }

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly CounterContext _counterContext;
        private readonly IMapper _mapper;

        public Handler(CounterContext counterContext, IMapper mapper)
        {
            _counterContext = counterContext;
            _mapper = mapper;
        }

        public async Task<Result> Handle(Command request, CancellationToken ct)
        {
            var counter = _mapper.Map<Counter>(request);

            await _counterContext.Counters.AddAsync(counter, ct);
            await _counterContext.SaveChangesAsync(ct);

            var created = _mapper.Map<CounterDto>(counter);
            var result = new Result { CreatedCounter = created };
            return result;
        }
    }
}
