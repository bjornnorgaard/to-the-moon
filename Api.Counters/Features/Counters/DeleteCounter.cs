using Api.Counters.Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Counters.Features.Counters;

public class DeleteCounter
{
    public class Command : IRequest
    {
        public Guid CounterId { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.CounterId).NotEmpty();
        }
    }

    public class Handler : AsyncRequestHandler<Command>
    {
        private readonly CounterContext _counterContext;

        public Handler(CounterContext counterContext)
        {
            _counterContext = counterContext;
        }

        protected override async Task Handle(Command request, CancellationToken ct)
        {
            var todo = await _counterContext.Counters.AsTracking()
                .Where(t => t.Id == request.CounterId)
                .FirstOrDefaultAsync(ct);

            if (todo == null) return;

            _counterContext.Counters.Remove(todo);
            await _counterContext.SaveChangesAsync(ct);
        }
    }
}
