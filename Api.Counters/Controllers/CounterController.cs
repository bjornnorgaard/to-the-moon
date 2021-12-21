using Ant.Platform.Hangfire;
using Api.Counters.Features.Counters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Counters.Controllers;

[ApiController]
public class CounterController : ControllerBase
{
    private readonly IMediator _mediator;

    public CounterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Routes.Counters.GetCounter)]
    public async Task<GetCounter.Result> GetCounter(
        [FromBody] GetCounter.Command command,
        CancellationToken ct)
    {
        return await _mediator.Send(command, ct);
    }

    [HttpPost(Routes.Counters.GetCounters)]
    public async Task<GetCounters.Result> GetCounters(
        [FromBody] GetCounters.Command command,
        CancellationToken ct)
    {
        return await _mediator.Send(command, ct);
    }

    [HttpPost(Routes.Counters.CreateCounter)]
    public async Task<CreateCounter.Result> CreateCounter(
        [FromBody] CreateCounter.Command command,
        CancellationToken ct)
    {
        return await _mediator.Send(command, ct);
    }

    [HttpPost(Routes.Counters.IncrementCounter)]
    public async Task<IncrementCounter.Result> IncrementCounter(
        [FromBody] IncrementCounter.Command command,
        CancellationToken ct)
    {
        return await _mediator.Send(command, ct);
    }

    [HttpPost(Routes.Counters.DecrementCounter)]
    public async Task<DecrementCounter.Result> DecrementCounter(
        [FromBody] DecrementCounter.Command command,
        CancellationToken ct)
    {
        return await _mediator.Send(command, ct);
    }

    [HttpPost(Routes.Counters.DeleteCounter)]
    public AcceptedResult DeleteCounter(
        [FromBody] DeleteCounter.Command command)
    {
        _mediator.Enqueue(command);
        return Accepted();
    }
}
