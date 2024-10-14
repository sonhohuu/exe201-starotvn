using Exe.Starot.Application.FavoriteProduct;
using Exe.Starot.Application.FavoriteProduct.Create;
using Exe.Starot.Application.FavoriteProduct.Get;
using Exe.Starot.Domain.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Exe.Starot.Api.Controllers
{
    [ApiController]
    [Route("api/v1/favorite-products")]
    public class FavoriteProductController : ControllerBase
    {
        private readonly ISender _mediator;

        public FavoriteProductController(ISender mediator)
        {
            _mediator = mediator;
        }

        // POST: api/v1/favorite-products
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddFavoriteProduct(
            [FromBody] AddFavoriteProductCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(StatusCodes.Status200OK, result, ""));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new JsonResponse<string>(StatusCodes.Status404NotFound, ex.Message, ""));
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(new JsonResponse<string>(StatusCodes.Status401Unauthorized, ex.Message, ""));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, ""));
            }
        }

        // GET: api/v1/favorite-products/user/{userId}
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<FavoriteProductDto>>> GetUserFavoriteProducts(
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetFavoriteProductsByUserQuery();
                var result = await _mediator.Send(query, cancellationToken);
                return Ok(new JsonResponse<IEnumerable<FavoriteProductDto>>(StatusCodes.Status200OK, "Get Success", result));
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(new JsonResponse<string>(StatusCodes.Status401Unauthorized, ex.Message, ""));
            }
        }
    }
}
