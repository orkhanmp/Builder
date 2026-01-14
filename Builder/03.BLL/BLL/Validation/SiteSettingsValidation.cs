using Entities.TableModels.Content;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class SiteSettingsValidation : AbstractValidator<SiteSettings>
    {
        public SiteSettingsValidation()
        {
            RuleFor(x => x.CompanyName)
                .MaximumLength(200)
                .WithMessage("Company name max 200 characters");

            RuleFor(x => x.LogoUrl)
                .MaximumLength(500)
                .WithMessage("Logo URL max 500 characters");

            RuleFor(x => x.FaviconUrl)
                .MaximumLength(500)
                .WithMessage("Favicon URL max 500 characters");

            RuleFor(x => x.OpeningHours)
                .MaximumLength(200)
                .WithMessage("Opening hours max 200 characters");

            RuleFor(x => x.Phone)
                .MaximumLength(50)
                .WithMessage("Phone max 50 characters");

            RuleFor(x => x.Email)
                .MaximumLength(200)
                .WithMessage("Email max 200 characters")
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Invalid email format");

            RuleFor(x => x.Address)
                .MaximumLength(500)
                .WithMessage("Address max 500 characters");

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

            RuleFor(x => x.YouTubeUrl)
                .MaximumLength(500)
                .WithMessage("YouTube URL max 500 characters");

            RuleFor(x => x.GoogleMapEmbedUrl)
                .MaximumLength(1000)
                .WithMessage("Google map embed URL max 1000 characters");

            RuleFor(x => x.Latitude)
                .MaximumLength(50)
                .WithMessage("Latitude max 50 characters");

            RuleFor(x => x.Longitude)
                .MaximumLength(50)
                .WithMessage("Longitude max 50 characters");

            RuleFor(x => x.MetaTitle)
                .MaximumLength(200)
                .WithMessage("Meta title max 200 characters");

            RuleFor(x => x.MetaDescription)
                .MaximumLength(500)
                .WithMessage("Meta description max 500 characters");

            RuleFor(x => x.MetaKeywords)
                .MaximumLength(500)
                .WithMessage("Meta keywords max 500 characters");
        }
    }
}
