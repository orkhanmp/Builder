using Entities.TableModels.Content;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class HomeSliderValidation : AbstractValidator<HomeSlider>
    {
        public HomeSliderValidation()
        {
            RuleFor(x => x.Title)
                .MaximumLength(200)
                .WithMessage("Subtitle max 200 characters");

            RuleFor(x => x.Heading)
                .NotEmpty()
                .WithMessage("Main heading cannot be empty")
                .MaximumLength(500)
                .WithMessage("Main heading max 500 characters");

            RuleFor(x => x.ButtonText)
                .MaximumLength(100)
                .WithMessage("Button text max 100 characters");

            RuleFor(x => x.ButtonUrl)
                .MaximumLength(500)
                .WithMessage("Button URL max 500 characters");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500)
                .WithMessage("Background image URL max 500 characters");

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Display order must be 0 or greater");
        }
    }

}
