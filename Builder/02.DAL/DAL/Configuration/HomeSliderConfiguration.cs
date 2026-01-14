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
    public class HomeSliderConfiguration : IEntityTypeConfiguration<HomeSlider>
    {
        public void Configure(EntityTypeBuilder<HomeSlider> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.Title).HasMaxLength(200);
            builder.Property(x => x.Heading).HasMaxLength(500).IsRequired();
            builder.Property(x => x.ButtonText).HasMaxLength(100);
            builder.Property(x => x.ButtonUrl).HasMaxLength(500);
            builder.Property(x => x.ImageUrl).HasMaxLength(500);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
