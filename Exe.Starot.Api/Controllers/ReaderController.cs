using Exe.Starot.Application.Customer.Commands.UpdateCustomer;
using Exe.Starot.Application.Customer.Queries.Filter;
using Exe.Starot.Application.Customer.Queries.GetById;
using Exe.Starot.Application.Customer;
using Exe.Starot.Domain.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Exe.Starot.Application.Reader.Commands.UpdateReader;
using Exe.Starot.Application.Reader.Queries.FilterReader;
using Exe.Starot.Application.Reader.Queries.GetById;
using Exe.Starot.Application.Reader;

namespace Exe.Starot.Api.Controllers
{
    [ApiController]
    [Route("api/v1/reader")]
    public class ReaderController : ControllerBase
    {
        private readonly ISender _mediator;

        public ReaderController(ISender mediator)
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
        public async Task<ActionResult> UpdateReader(
            [FromForm] UpdateReaderCommand command,
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
        public async Task<ActionResult> FiltereReader(
            [FromQuery] FilterReaderQuery query,
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

        [HttpGet("{readerId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetReaderById(
            [FromRoute] string readerId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(new GetReaderByIdQuery { Id = readerId }, cancellationToken);

                if (result == null)
                {
                    return NotFound(new JsonResponse<string>(StatusCodes.Status404NotFound, "PackageQuestion not found.", ""));
                }

                return Ok(new JsonResponse<ReaderDTO>(StatusCodes.Status200OK, "Get Sucess", result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, ""));
            }
        }
    }
}
