using Entities.TableModels.Content;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class PageBannerValidation : AbstractValidator<PageBanner>
    {
        public PageBannerValidation()
        {
            RuleFor(x => x.PageName)
                .NotEmpty()
                .WithMessage("Page name cannot be empty")
                .Length(3, 100)
                .WithMessage("Page name must be between 3 and 100 characters");

            RuleFor(x => x.Title)
                .MaximumLength(300)
                .WithMessage("Title max 300 characters");

            RuleFor(x => x.Breadcrumb)
                .MaximumLength(300)
                .WithMessage("Breadcrumb max 300 characters");

            RuleFor(x => x.BackgroundImageUrl)
                .MaximumLength(500)
                .WithMessage("Background image URL max 500 characters");

            RuleFor(x => x.BackgroundColor)
                .MaximumLength(50)
                .WithMessage("Background color max 50 characters");
        }
    }
}
