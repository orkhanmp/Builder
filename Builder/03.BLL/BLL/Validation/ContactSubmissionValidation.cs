using Entities.TableModels.Content;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class ContactSubmissionValidation : AbstractValidator<ContactSubmission>
    {
        public ContactSubmissionValidation()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name cannot be empty")
                .Length(3, 200)
                .WithMessage("Full name must be between 3 and 200 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty")
                .EmailAddress()
                .WithMessage("Invalid email format")
                .MaximumLength(200)
                .WithMessage("Email max 200 characters");

            RuleFor(x => x.Subject)
                .MaximumLength(300)
                .WithMessage("Subject max 300 characters");

            RuleFor(x => x.Message)
                .NotEmpty()
                .WithMessage("Message cannot be empty")
                .Length(10, 2000)
                .WithMessage("Message must be between 10 and 2000 characters");
        }
    }
}
