using Entities.TableModels.Content;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class TeamMemberValidation : AbstractValidator<TeamMember>
    {
        public TeamMemberValidation()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name cannot be empty")
                .Length(3, 200)
                .WithMessage("Full name must be between 3 and 200 characters");

            RuleFor(x => x.Position)
                .MaximumLength(200)
                .WithMessage("Position max 200 characters");

            RuleFor(x => x.Bio)
                .MaximumLength(2000)
                .WithMessage("Bio max 2000 characters");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500)
                .WithMessage("Image URL max 500 characters");

            RuleFor(x => x.Email)
                .MaximumLength(200)
                .WithMessage("Email max 200 characters")
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Invalid email format");

            RuleFor(x => x.Phone)
                .MaximumLength(50)
                .WithMessage("Phone max 50 characters");

            RuleFor(x => x.FacebookUrl)
                .MaximumLength(500)
                .WithMessage("Facebook URL max 500 characters");

            RuleFor(x => x.TwitterUrl)
                .MaximumLength(500)
                .WithMessage("Twitter URL max 500 characters");

            RuleFor(x => x.LinkedInUrl)
                .MaximumLength(500)
                .WithMessage("LinkedIn URL max 500 characters");

            RuleFor(x => x.InstagramUrl)
                .MaximumLength(500)
                .WithMessage("Instagram URL max 500 characters");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Display order must be 0 or greater");
        }
    }
}
