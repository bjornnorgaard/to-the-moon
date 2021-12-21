using Ant.Platform.Exceptions;
using Api.Counters.Database;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Counters.Features.Counters;

public class GetCounter
{
    public class Command : IRequest<Result>
    {
        public Guid CounterId { get; set; }
    }

    public class Result
    {
        public CounterDto Counter { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.CounterId).NotEmpty();
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
            var counter = await _counterContext.Counters.AsNoTracking()
                .Where(t => t.Id == request.CounterId)
                .FirstOrDefaultAsync(ct);

            if (counter == null) throw new PlatformException(PlatformError.CounterNotFound);

            var mapped = _mapper.Map<CounterDto>(counter);
            var result = new Result { Counter = mapped };

            return result;
        }
    }
}
