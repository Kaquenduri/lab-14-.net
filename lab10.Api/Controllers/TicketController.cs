using lab10.Application.DTOs;
using lab10.Application.UsesCases.Ticket.Command;
using lab10.Application.UsesCases.Ticket.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace lab10.Api.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await mediator.Send(new GetAllTicketUseCaseQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await mediator.Send(new GetByIdTicketUseCaseQuery(id)));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketUseCaseCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTicketUseCaseCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await mediator.Send(new DeleteTicketUseCaseCommand(id)));
    }
}
