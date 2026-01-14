using Entities.TableModels.Content;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class AboutValidation : AbstractValidator<About>
    {
        public AboutValidation()
        {
            RuleFor(x => x.SubTitle)
                .MaximumLength(200)
                .WithMessage("Subtitle max 200 characters");

            RuleFor(x => x.MainHeading)
                .NotEmpty()
                .WithMessage("Main heading cannot be empty")
                .MaximumLength(500)
                .WithMessage("Main heading max 500 characters");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500)
                .WithMessage("Image URL max 500 characters");

            RuleFor(x => x.ButtonText)
                .MaximumLength(100)
                .WithMessage("Button text max 100 characters");

            RuleFor(x => x.ButtonUrl)
                .MaximumLength(500)
                .WithMessage("Button URL max 500 characters");

            RuleFor(x => x.ExpertWorkers)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Expert workers must be 0 or greater");

            RuleFor(x => x.HappyClients)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Happy clients must be 0 or greater");

            RuleFor(x => x.CompletedProjects)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Completed projects must be 0 or greater");

            RuleFor(x => x.RunningProjects)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Running projects must be 0 or greater");
        }
    }

}
