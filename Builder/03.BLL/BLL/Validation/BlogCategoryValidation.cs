using Entities.TableModels.Content;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class BlogCategoryValidation : AbstractValidator<BlogCategory>
    {
        public BlogCategoryValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Blog category name cannot be empty")
                .Length(3, 200)
                .WithMessage("Blog category name must be between 3 and 200 characters");

            RuleFor(x => x.Slug)
                .NotEmpty()
                .WithMessage("Slug cannot be empty")
                .Length(3, 250)
                .WithMessage("Slug must be between 3 and 250 characters")
                .Matches("^[a-z0-9-]+$")
                .WithMessage("Slug can only contain lowercase letters, numbers and hyphens");

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .WithMessage("Description max 1000 characters");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Display order must be 0 or greater");
        }
    }
}
