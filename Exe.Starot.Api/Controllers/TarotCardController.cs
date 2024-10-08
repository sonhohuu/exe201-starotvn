using Exe.Starot.Api.Attributes;
using Exe.Starot.Api.Services;
using Exe.Starot.Application.Common.Pagination;
using Exe.Starot.Application.TarotCard;
using Exe.Starot.Application.TarotCard.Create;
using Exe.Starot.Application.TarotCard.Delete;
using Exe.Starot.Application.TarotCard.Filter;
using Exe.Starot.Application.TarotCard.Get;
using Exe.Starot.Application.TarotCard.Update;
using Exe.Starot.Domain.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Exe.Starot.Api.Controllers
{
    [ApiController]
    [Route("api/v1/tarotcards")]
    public class TarotCardController : ControllerBase
    {
        private readonly IResponseCacheService _responseCacheService;
        private readonly ISender _mediator;

        public TarotCardController(ISender mediator, IResponseCacheService responseCacheService)
        {
            _mediator = mediator;
            _responseCacheService = responseCacheService;
        }

        // POST: api/v1/tarotcards
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateTarotCard(
            [FromForm] CreateTarotCardCommand command,
            CancellationToken cancellationToken = default)
        {
            await _responseCacheService.RemoveCacheResponseAsync("api/v1/tarotcards");       // GetAll cache
            await _responseCacheService.RemoveCacheResponseAsync("api/v1/tarotcards/random"); // Random card cache


            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return CreatedAtAction(nameof(CreateTarotCard), new { id = result },
                    new JsonResponse<string>(StatusCodes.Status201Created, result, "Create "));
            }
            catch (DuplicationException ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, ex.Message, ""));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, ""));
            }
        }

        // GET: api/v1/tarotcards/random
        [HttpGet("random")]
        [Cache(1000)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetRandomTarotCard(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(new GetRandomTarotCardQuery(), cancellationToken);
                return Ok(new JsonResponse<TarotCardDto>(StatusCodes.Status200OK, "Random tarot card fetched successfully.", result));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new JsonResponse<string>(StatusCodes.Status404NotFound, ex.Message, ""));
            }
        }

        // GET: api/v1/tarotcards
        [HttpGet]
        [Cache(1000)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> FilterTarotCards(
            [FromQuery] FilterTarotCardQuery query,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(query, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, ex.Message, ""));
            }
        }

        // PUT: api/v1/tarotcards
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTarotCard(
            [FromBody] UpdateCardCommand command,
            CancellationToken cancellationToken = default)
        {
            await _responseCacheService.RemoveCacheResponseAsync("api/v1/tarotcards");       // GetAll cache
            await _responseCacheService.RemoveCacheResponseAsync("api/v1/tarotcards/random"); // Random card cache
            if (command.Id <= 0)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, "Invalid ID provided.", ""));
            }

            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(StatusCodes.Status200OK, result, ""));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new JsonResponse<string>(StatusCodes.Status404NotFound, ex.Message, ""));
            }
            catch (DuplicationException ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, ex.Message, ""));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, ""));
            }
        }

        // DELETE: api/v1/tarotcards/{id}
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTarotCard(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            await _responseCacheService.RemoveCacheResponseAsync("api/v1/tarotcards");       // GetAll cache
            await _responseCacheService.RemoveCacheResponseAsync("api/v1/tarotcards/random"); // Random card cache
            try
            {
                var command = new DeleteTarotCardCommand { Id = id };
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(StatusCodes.Status200OK, result, ""));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new JsonResponse<string>(StatusCodes.Status404NotFound, ex.Message, ""));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, ""));
            }
        }
    }

}
