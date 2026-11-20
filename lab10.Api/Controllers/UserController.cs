using lab10.Application.DTOs;
using lab10.Application.UsesCases.UserEntity;
using lab10.Application.UsesCases.UserEntity.Command;
using lab10.Application.UsesCases.UserEntity.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace lab10.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IMediator _mediator) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllUserUserCaseQuery())); 
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _mediator.Send(new GetByIdUserUseCaseQuery(id))); 
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserUseCaseCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserUseCaseCommand request)
    {
        return Ok(await _mediator.Send(request)); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await _mediator.Send(new DeleteUserUseCaseCommand(id)));
    }
}