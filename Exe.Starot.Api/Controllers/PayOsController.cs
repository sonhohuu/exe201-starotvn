using Azure;
using Exe.Starot.Application.PayOs;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;

namespace Exe.Starot.Api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class PayOsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly PayOsServices _payOsServices;
        private readonly ITarotCardRepository _cardRepository;

        public PayOsController(ISender mediator, PayOsServices payOsServices, ITarotCardRepository tarotCardRepository)
        {
            _mediator = mediator;
            _payOsServices = payOsServices;
            _cardRepository = tarotCardRepository;
        }

        [HttpPost("payOs")]
        public async Task<IActionResult> CreatePaymentLink([FromBody] PaymentRequest model)
        {
            if (model == null)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, "Model not found", ""));
            }

            try
            {
                string paymentLink = await _payOsServices.CreatePaymentLink(model);
                return Ok(new JsonResponse<object>(StatusCodes.Status200OK, "Create success", new { Url = paymentLink }));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new JsonResponse<string>(StatusCodes.Status401Unauthorized, ex.Message, ""));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, ""));
            }
        }

        [HttpPost("hook")]
        public async Task<IActionResult> ReceiveWebhook([FromBody] WebhookType webhookBody)
        {



            try
            {
                var result = await _payOsServices.ProcessPaymentResponse(webhookBody);

                if (result.IsSuccess)
                {
                    return Ok(new { Message = "Webhook processed successfully", TransactionId = result.Code });
                }

                return BadRequest(new { Message = "Webhook processing failed.", Code = result.Code });

            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, ex.Message, ""));
            }
        }

    }
}
