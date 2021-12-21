using Api.Counters.Database;
using Api.Counters.Database.Extensions;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Counters.Features.Counters;

public class GetCounters
{
    public class Command : IRequest<Result>
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string SortProperty { get; set; } = nameof(CounterDto.Id);
        public SortOrder SortOrder { get; set; } = SortOrder.None;
    }

    public class Result
    {
        public List<CounterDto> Counters { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.PageNumber)
                .GreaterThanOrEqualTo(0);

            RuleFor(c => c.PageSize).NotEmpty()
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);

            RuleFor(c => c.SortOrder).IsInEnum();
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
            var counters = await _counterContext.Counters.AsNoTracking()
                .SortBy(CounterSortExpressions.Get(request.SortProperty), request.SortOrder)
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(ct);

            var mapped = _mapper.Map<List<CounterDto>>(counters);
            var result = new Result { Counters = mapped };

            return result;
        }
    }
}
