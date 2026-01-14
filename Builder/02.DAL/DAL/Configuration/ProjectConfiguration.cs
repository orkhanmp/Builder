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
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.Title).HasMaxLength(300).IsRequired();
            builder.Property(x => x.ShortDescription).HasMaxLength(500);
            builder.Property(x => x.DetailedDescription);
            builder.Property(x => x.Location).HasMaxLength(300);
            builder.Property(x => x.ClientName).HasMaxLength(200);
            builder.Property(x => x.Budget).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Status).HasMaxLength(50);
            builder.Property(x => x.MainImageUrl).HasMaxLength(500);
            builder.Property(x => x.GalleryImagesJson).HasColumnType("nvarchar(max)");
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
