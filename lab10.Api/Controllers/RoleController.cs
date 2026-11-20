using lab10.Application.DTOs;
using lab10.Application.UsesCases.Role.Command;
using lab10.Application.UsesCases.Role.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace lab10.Api.Controllers;

[ApiController]
[Route("api/roles")]
public class RoleController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await mediator.Send(new GetAllRoleUseCaseQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await mediator.Send(new GetByIdRoleUseCaseQuery(id)));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleUseCaseCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRoleUseCaseCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await mediator.Send(new DeleteRoleUseCaseCommand(id)));
    }
}
