using Parameters.Application.Request.Command.BankAccount;

namespace Parameters.Application.Request.Validation.BankAccount;

public class CreateCommandValidation : AbstractValidator<CreateRequestCommand>
{
    public CreateCommandValidation()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("{property} can't be null")
            .NotEmpty().WithMessage("{property} can't be empty");
    }
}