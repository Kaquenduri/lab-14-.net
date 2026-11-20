using lab10.Application.DTOs;
using lab10.Application.UsesCases.UserRole.Command;
using lab10.Application.UsesCases.UserRole.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lab10.Api.Controllers;

[ApiController]
[Route("api/users-roles")]

public class UserRoleController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await mediator.Send(new GetAllUserRoleUseCaseQuery()));
    }

    [HttpGet("{userId}/{roleId}")]
    public async Task<IActionResult> GetById(Guid userId, Guid roleId)
    {
        return Ok(await mediator.Send(new GetByIdUserRoleUseCaseQuery(userId, roleId)));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRoleUseCaseCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRoleUseCaseCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpDelete("{userId}/{roleId}")]
    public async Task<IActionResult> Delete(Guid userId, Guid roleId)
    {
        return Ok(await mediator.Send(new DeleteUserRoleUseCaseCommand(userId, roleId)));
    }
}
