using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using User.API.Authorization;
using User.API.Models;
using User.Application.Commands;
using User.Application.DTO;
using User.Application.Queries;

namespace User.API.Controllers.v1
{
    [ApiController]    
    [Route("api/user")]
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResultController), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultController), StatusCodes.Status400BadRequest)]
        
        public async Task<IActionResult> CreateUser([FromForm, Required] CreateUserCommand userCommand,
            CancellationToken cancellationToken)
        {
            Guid guid = await _mediator.Send(userCommand, cancellationToken).ConfigureAwait(false);
            return Ok(new { id_usuario = guid, mensagem = "Usuario Criado Com sucesso." });
        }

        [HttpDelete("{idUser}")]
        [ProducesResponseType(typeof(ResultController), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultController), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DisableUser([FromBody,Required] DisableUserCommand disableUser, 
            Guid idUser, CancellationToken cancellationToken) 
        {
            disableUser.idUser = idUser;
            await _mediator.Send(disableUser, cancellationToken).ConfigureAwait(false);

            return Ok("Usuario desativado com sucesso.");
        }


        [HttpGet("{idUser}")]
        [ProducesResponseType(typeof(UserAcessDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultController), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserAcess(Guid idUser, CancellationToken cancellationToken)
        {
            UserAcessDTO user = await _mediator.Send(new GetUserQuerie { Id = idUser }
                ,cancellationToken).ConfigureAwait(false);

            return Ok(user);
        }

        [HttpGet("all/{range}")]
        [ProducesResponseType(typeof(List<UserAcessDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultController), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllUser(int range, CancellationToken cancellationToken)
        {
            List<UserAcessDTO> acessDTOs = await _mediator.Send(new GetAllUserQuerie { Range  = range}, 
                cancellationToken).ConfigureAwait(false);

            return Ok(acessDTOs);
        }

    }
}
