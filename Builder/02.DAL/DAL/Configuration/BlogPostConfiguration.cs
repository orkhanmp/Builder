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
    public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.Title).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(550).IsRequired();
            builder.Property(x => x.Summary).HasMaxLength(1000);
            builder.Property(x => x.Content).HasColumnType("nvarchar(max)");
            builder.Property(x => x.FeaturedImageUrl).HasMaxLength(500);
            builder.Property(x => x.AuthorName).HasMaxLength(200);
            builder.Property(x => x.AuthorImageUrl).HasMaxLength(500);
            builder.Property(x => x.Tags).HasMaxLength(500);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasIndex(x => x.Slug).IsUnique().HasDatabaseName("idx_BlogPost_Slug");
        }
    }
}
