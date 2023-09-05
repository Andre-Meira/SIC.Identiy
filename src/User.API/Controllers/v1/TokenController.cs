using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.API.Models;
using User.Application.Commands;

namespace User.API.Controllers.v1
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : BaseApiController
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(ResultController), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultController), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerateToken(CancellationToken cancellationToken)
        {
            string? auth = Request.Headers.Authorization;

            BasicAuthorization basicAuthorization = new BasicAuthorization(auth);
            var command = new CreateTokenCommand(basicAuthorization.Username!, basicAuthorization.Password!);

            string Token = await _mediator.Send(command, cancellationToken);

            return Ok(new {token_acess = Token, expiration = DateTime.Now.AddHours(2)});
        }
    }
}
