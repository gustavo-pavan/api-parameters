using Parameters.Application.Request.Command.PaymentType;

namespace Parameters.Application.Request.Validation.PaymentType;

public class UpdateCommandValidation : AbstractValidator<UpdatePaymentTypeRequestCommand>
{
    public UpdateCommandValidation()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("{property} can't be null")
            .NotEmpty().WithMessage("{property} can't be empty");

        RuleFor(x => x.Id)
            .NotNull().WithMessage("{property} can't be null")
            .NotEmpty().WithMessage("{property} can't be empty");
    }
}