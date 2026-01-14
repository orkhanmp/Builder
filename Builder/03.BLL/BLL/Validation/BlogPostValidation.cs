using Entities.TableModels.Content;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class BlogPostValidation : AbstractValidator<BlogPost>
    {
        public BlogPostValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Blog post title cannot be empty")
                .Length(3, 500)
                .WithMessage("Blog post title must be between 3 and 500 characters");

            RuleFor(x => x.Slug)
                .NotEmpty()
                .WithMessage("Slug cannot be empty")
                .Length(3, 550)
                .WithMessage("Slug must be between 3 and 550 characters")
                .Matches("^[a-z0-9-]+$")
                .WithMessage("Slug can only contain lowercase letters, numbers and hyphens");

            RuleFor(x => x.Summary)
                .MaximumLength(1000)
                .WithMessage("Summary max 1000 characters");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content cannot be empty");

            RuleFor(x => x.FeaturedImageUrl)
                .MaximumLength(500)
                .WithMessage("Featured image URL max 500 characters");

            RuleFor(x => x.AuthorName)
                .MaximumLength(200)
                .WithMessage("Author name max 200 characters");

            RuleFor(x => x.AuthorImageUrl)
                .MaximumLength(500)
                .WithMessage("Author image URL max 500 characters");

            RuleFor(x => x.Tags)
                .MaximumLength(500)
                .WithMessage("Tags max 500 characters");

            RuleFor(x => x.BlogCategoryId)
                .GreaterThan(0)
                .WithMessage("Please select a blog category");

            RuleFor(x => x.PublishDate)
                .NotEmpty()
                .WithMessage("Publish date cannot be empty");
        }
    }
}
