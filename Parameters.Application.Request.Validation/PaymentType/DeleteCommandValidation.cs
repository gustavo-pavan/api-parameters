using Parameters.Application.Request.Command.PaymentType;

namespace Parameters.Application.Request.Validation.PaymentType;

public class DeleteCommandValidation : AbstractValidator<DeleteRequestCommand>
{
    public DeleteCommandValidation()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("{property} can't be null")
            .NotEmpty().WithMessage("{property} can't be empty");
    }
}