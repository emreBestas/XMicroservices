using FluentValidation;
using X.Web.Models.Discounts;

namespace X.Web.Validators
{
    public class DiscountApplyInputValidator:AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("This field cannot be emty");
        }
    }
}
