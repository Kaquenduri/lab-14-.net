using lab10.Application.DTOs;
using lab10.Application.UsesCases.Response.Command;
using lab10.Application.UsesCases.Response.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace lab10.Api.Controllers;

[ApiController]
[Route("api/responses")]
public class ResponseController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await mediator.Send(new GetAllResponseUseCaseQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await mediator.Send(new GetByIdResponseUseCaseQuery(id)));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResponseUseCaseCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateResponseUseCaseCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await mediator.Send(new DeleteResponseUseCaseCommand(id)));
    }
}
