using Microsoft.AspNetCore.Mvc;
using Parameters.Application.Request.Command.PaymentType;

namespace Parameters.Presentation.Api.Controllers;

[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status409Conflict)]
[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[Route("api/payment-type")]
[ApiController]
public class PaymentTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetPaymentTypeGetRequestCommand());

        if (result?.Count() == default)
            return NoContent();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetByIdPaymentTypeRequestCommand { Id = id });
        if (result is not null)
            return Ok(result);

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePaymentTypeRequestCommand paymentTypeRequest)
    {
        var result = await _mediator.Send(paymentTypeRequest);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdatePaymentTypeRequestCommand paymentTypeRequest)
    {
        var result = await _mediator.Send(paymentTypeRequest);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeletePaymentTypeRequestCommand { Id = id });
        return Ok(result);
    }
}