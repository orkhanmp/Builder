using Entities.TableModels.Content;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class ServiceValidation : AbstractValidator<Service>
    {
        public ServiceValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Service title cannot be empty")
                .Length(3, 300)
                .WithMessage("Service title must be between 3 and 300 characters");

            RuleFor(x => x.ShortDescription)
                .MaximumLength(500)
                .WithMessage("Short description max 500 characters");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500)
                .WithMessage("Image URL max 500 characters");

            RuleFor(x => x.IconClass)
                .MaximumLength(100)
                .WithMessage("Icon class max 100 characters");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Display order must be 0 or greater");
        }
    }
}
