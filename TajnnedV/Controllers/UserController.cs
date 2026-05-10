using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tajnned.Application.DTOs;
using Tajnned.Application.user;
using Tajnned.Application.user.Model;

namespace Tajnned.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        readonly IMediator _mediator;

        public UserController(IMediator mediator) => _mediator = mediator;

        [HttpPost(GetTokenHandler.ROUTE)]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetToken([FromBody] LoginDto LoginRequest, CancellationToken cancellationToken)
         => Ok(await _mediator.Send(new GetTokenRequest(LoginRequest.Username, LoginRequest.Password), cancellationToken));
         
    }
}
