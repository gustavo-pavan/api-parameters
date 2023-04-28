using Parameters.Application.Request.Command.FlowParameter;

namespace Parameters.Application.Request.Validation.FlowParameter;

public class GetByIdCommandValidation : AbstractValidator<GetByIdFlowParameterRequestCommand>
{
    public GetByIdCommandValidation()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("{property} can't be null")
            .NotEmpty().WithMessage("{property} can't be empty");
    }
}