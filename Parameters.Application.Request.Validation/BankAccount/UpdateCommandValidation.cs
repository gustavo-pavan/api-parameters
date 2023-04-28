using Parameters.Application.Request.Command.BankAccount;

namespace Parameters.Application.Request.Validation.BankAccount;

public class UpdateCommandValidation : AbstractValidator<UpdateBankAccountRequestCommand>
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