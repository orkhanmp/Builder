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
    public class PageBannerConfiguration : IEntityTypeConfiguration<PageBanner>
    {
        public void Configure(EntityTypeBuilder<PageBanner> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.PageName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(300);
            builder.Property(x => x.Breadcrumb).HasMaxLength(300);
            builder.Property(x => x.BackgroundImageUrl).HasMaxLength(500);
            builder.Property(x => x.BackgroundColor).HasMaxLength(50);
            builder.Property(x => x.UpdatedDate).HasDefaultValueSql("GETDATE()");

            builder.HasIndex(x => x.PageName).IsUnique().HasDatabaseName("idx_PageBanner_PageName");
        }
    }
}
