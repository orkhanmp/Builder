using Entities.TableModels.Content;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class ProjectValidation : AbstractValidator<Project>
    {
        public ProjectValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Project title cannot be empty")
                .Length(3, 300)
                .WithMessage("Project title must be between 3 and 300 characters");

            RuleFor(x => x.ShortDescription)
                .MaximumLength(500)
                .WithMessage("Short description max 500 characters");

            RuleFor(x => x.Location)
                .MaximumLength(300)
                .WithMessage("Location max 300 characters");

            RuleFor(x => x.ClientName)
                .MaximumLength(200)
                .WithMessage("Client name max 200 characters");

            RuleFor(x => x.Budget)
                .GreaterThan(0)
                .When(x => x.Budget.HasValue)
                .WithMessage("Budget must be greater than 0");

            RuleFor(x => x.Status)
                .MaximumLength(50)
                .WithMessage("Status max 50 characters");

            RuleFor(x => x.MainImageUrl)
                .MaximumLength(500)
                .WithMessage("Main image URL max 500 characters");

            RuleFor(x => x.ProjectCategoryId)
                .GreaterThan(0)
                .WithMessage("Please select a project category");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("End date must be after start date");
        }
    }
}
