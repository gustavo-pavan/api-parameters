using Parameters.Application.Request.Command.BankAccount;

namespace Parameters.Application.Request.Validation.BankAccount;

public class GetByIdCommandValidation : AbstractValidator<GetByIdBankAccountRequestCommand>
{
    public GetByIdCommandValidation()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("{property} can't be null")
            .NotEmpty().WithMessage("{property} can't be empty");
    }
}