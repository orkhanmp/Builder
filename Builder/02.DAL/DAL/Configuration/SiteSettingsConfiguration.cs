using Entities.TableModels.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configuration
{
    public class SiteSettingsConfiguration : IEntityTypeConfiguration<SiteSettings>
    {
        public void Configure(EntityTypeBuilder<SiteSettings> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.CompanyName).HasMaxLength(200);
            builder.Property(x => x.LogoUrl).HasMaxLength(500);
            builder.Property(x => x.FaviconUrl).HasMaxLength(500);
            builder.Property(x => x.OpeningHours).HasMaxLength(200);
            builder.Property(x => x.Phone).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(200);
            builder.Property(x => x.Address).HasMaxLength(500);
            builder.Property(x => x.FacebookUrl).HasMaxLength(500);
            builder.Property(x => x.TwitterUrl).HasMaxLength(500);
            builder.Property(x => x.LinkedInUrl).HasMaxLength(500);
            builder.Property(x => x.InstagramUrl).HasMaxLength(500);
            builder.Property(x => x.YouTubeUrl).HasMaxLength(500);
            builder.Property(x => x.GoogleMapEmbedUrl).HasMaxLength(1000);
            builder.Property(x => x.Latitude).HasMaxLength(50);
            builder.Property(x => x.Longitude).HasMaxLength(50);
            builder.Property(x => x.MetaTitle).HasMaxLength(200);
            builder.Property(x => x.MetaDescription).HasMaxLength(500);
            builder.Property(x => x.MetaKeywords).HasMaxLength(500);
            builder.Property(x => x.UpdatedDate).HasDefaultValueSql("GETDATE()");
        }
    }
}
