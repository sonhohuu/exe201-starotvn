using Exe.Starot.Application.Order.Create;
using Exe.Starot.Application.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Google.Apis.Requests.BatchRequest;
using System.Net.Mime;
using Exe.Starot.Application.Booking.Commands.DeleteBooking;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Application.Order.UpdateOrder;
using Exe.Starot.Application.Booking.Queries.Filter;
using Exe.Starot.Application.Order.Filter;

namespace Exe.Starot.Api.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ISender mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateOrder(
          [FromBody] CreateOrderCommand command,
          CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(StatusCodes.Status200OK, result,""));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new JsonResponse<string>(StatusCodes.Status404NotFound, ex.Message, ""));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, ex.Message, ""));
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

        [HttpPut("{orderId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateOrder(
            string orderId,
            [FromBody] UpdateOrderCommand command,
            CancellationToken cancellationToken = default)
        {
            if (orderId != command.ID)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, "ID in route does not match ID in the request body.", ""));
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
            catch (ArgumentException ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, ex.Message, ""));
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

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> FilterOrder(
            [FromQuery] FilterOrderQuery query,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(query, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, ""));
            }
        }
    }
}
