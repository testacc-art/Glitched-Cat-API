using System;
using System.Threading.Tasks;
using AutoMapper;
using GlitchedCat.Application.Commands;
using GlitchedCat.Application.Queries.Blog;
using GlitchedCat.Domain.Common.Models.Blog;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GlitchedCat.API.Controllers.Blog
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);

            return Ok(_mapper.Map<UserResponse[]>(result));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserResponse>(result));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest userRequest)
        {
            var command = _mapper.Map<CreateUserCommand>(userRequest);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetUserById), new { id = result }, _mapper.Map<UserResponse>(result));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserRequest userRequest)
        {
            var command = _mapper.Map<UpdateUserCommand>(userRequest);
            command.Id = id;

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var user = await _mediator.Send(query);

            if (user == null)
            {
                return NotFound();
            }

            var command = new DeleteUserCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
