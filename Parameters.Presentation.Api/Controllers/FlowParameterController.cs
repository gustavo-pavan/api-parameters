using Microsoft.AspNetCore.Mvc;
using Parameters.Application.Request.Command.FlowParameter;

namespace Parameters.Presentation.Api.Controllers;

[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status409Conflict)]
[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[Route("api/flow-parameter")]
[ApiController]
public class FlowParameterController : ControllerBase
{
    private readonly IMediator _mediator;

    public FlowParameterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetFlowParameterRequestCommand());

        if (result?.Count() == default)
            return NoContent();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetByIdFlowParameterRequestCommand { Id = id });
        if (result is not null)
            return Ok(result);

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateFlowParameterRequestCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateFlowParameterUpdateRequestCommand flowParameterUpdateRequest)
    {
        var result = await _mediator.Send(flowParameterUpdateRequest);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteFlowParameterRequestCommand { Id = id });
        return Ok(result);
    }
}