﻿using Exe.Starot.Application.Dashboard.GetTotalPrice;
using Exe.Starot.Application.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Exe.Starot.Application.Dashboard.GetTotalProduct;

namespace Exe.Starot.Api.Controllers
{
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly ISender _mediator;

        public DashBoardController(ISender mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("total-prices")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetPackageTotalPrices(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(new GetPackageTotalPriceQuery(), cancellationToken);
                return Ok(new JsonResponse<List<PackageTotalPriceDTO>>(StatusCodes.Status200OK, "Total prices retrieved successfully", result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, ""));
            }
        }
        [HttpGet("product-sales-percentage")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetProductSalesPercentage([FromQuery] int month, [FromQuery] int year, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(new GetProductSalesPercentageQuery(month,year), cancellationToken);
                return Ok(new JsonResponse<List<ProductSalesPercentageDTO>>(StatusCodes.Status200OK, "Product sales percentage retrieved successfully", result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, ""));
            }
        }
    }
}
