using FluentValidation;
using X.Web.Models.Catalogs;

namespace X.Web.Validators
{
    public class CourseCreateInputValidator:AbstractValidator<CourseCreateInputViewModel>
    {
        public CourseCreateInputValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("This field cannot be left blank");
            RuleFor(x => x.Description).NotEmpty().WithMessage("This field cannot be left blank");
            RuleFor(x =>x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Duration field cannot be empty");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price field cannot be empty").ScalePrecision(2,6).WithMessage("Price format is wrong");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Select the category");
        }
    }
}
