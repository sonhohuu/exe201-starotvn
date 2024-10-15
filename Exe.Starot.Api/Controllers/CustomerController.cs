using Exe.Starot.Application.Booking.Commands.UpdateBooking;
using Exe.Starot.Application.Booking.Queries.Filter;
using Exe.Starot.Application.Booking.Queries.GetBookingById;
using Exe.Starot.Application.Booking;
using Exe.Starot.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using MediatR;
using Castle.Core.Resource;
using Exe.Starot.Application.Customer.Commands.UpdateCustomer;
using Exe.Starot.Application.Customer.Queries.GetById;
using Exe.Starot.Application.Customer;
using Exe.Starot.Application.Customer.Queries.Filter;

namespace Exe.Starot.Api.Controllers
{
    [ApiController]
    [Route("api/v1/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ISender _mediator;

        public CustomerController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPut()]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCustomer(
            [FromForm] UpdateCustomerCommand command,
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
        public async Task<ActionResult> FilterCustomer(
            [FromQuery] FilterCustomerQuery query,
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

        [HttpGet("info")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetCustomerById(
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(new GetCustomerByIdQuery(), cancellationToken);

                if (result == null)
                {
                    return NotFound(new JsonResponse<string>(StatusCodes.Status404NotFound, "PackageQuestion not found.", ""));
                }

                return Ok(new JsonResponse<CustomerWithInfoDTO>(StatusCodes.Status200OK, "Get Sucess", result));
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
    }
}
