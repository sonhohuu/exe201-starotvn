using Exe.Starot.Application.Common.Pagination;
using Exe.Starot.Domain.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Exe.Starot.Application.Booking.Commands.CreateBooking;
using Exe.Starot.Application.Booking.Commands.DeleteBooking;
using Exe.Starot.Application.Booking.Commands.UpdateBooking;
using Exe.Starot.Application.Booking.Queries.Filter;
using Exe.Starot.Application.Booking;
using Exe.Starot.Application.Booking.Queries.GetBookingById;

namespace Exe.Starot.Api.Controllers
{
    [ApiController]
    [Route("api/v1/booking")]
    public class BookingController : ControllerBase
    {
        private readonly ISender _mediator;

        public BookingController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateBooking(
            [FromForm] CreateBookingCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(StatusCodes.Status200OK, result, "")); ;
            }
            catch (DuplicationException ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, ex.Message, ""));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, "")));
            }
        }

        [HttpDelete("{bookingId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RemoveBooking(
            [FromRoute] string bookingId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new DeleteBookingCommand { BookingId = bookingId };
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(StatusCodes.Status200OK, result, ""));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new JsonResponse<string>(StatusCodes.Status404NotFound, ex.Message, ""));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, ""));
            }
        }

        [HttpPut("{bookingId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateBooking(
            string bookingId,
            [FromForm] UpdateBookingCommand command,
            CancellationToken cancellationToken = default)
        {
            if (bookingId != command.BookingId)
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
            catch (DuplicationException ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, ex.Message, ""));
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
        public async Task<ActionResult> FiltereBooking(
            [FromQuery] FilterBookingQuery query,
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

        [HttpGet("{bookingId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBookingById(
            [FromRoute] string bookingId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(new GetBookingByIdQuery { BookingId = bookingId }, cancellationToken);

                if (result == null)
                {
                    return NotFound(new JsonResponse<string>(StatusCodes.Status404NotFound,"Booking not found.",""));
                }

                return Ok(new JsonResponse<BookingDTO>(StatusCodes.Status200OK, "Get Sucess", result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, ""));
            }
        }


    }

}
