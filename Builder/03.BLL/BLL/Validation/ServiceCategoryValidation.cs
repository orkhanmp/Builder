using Entities.TableModels.Content;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class ServiceCategoryValidation : AbstractValidator<ServiceCategory>
    {
        public ServiceCategoryValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Category name cannot be empty")
                .Length(3, 200)
                .WithMessage("Category name must be between 3 and 200 characters");

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .WithMessage("Description max 1000 characters");

            RuleFor(x => x.IconClass)
                .MaximumLength(100)
                .WithMessage("Icon class max 100 characters");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Display order must be 0 or greater");
        }
    }
}
