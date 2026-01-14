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
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.Title).HasMaxLength(300).IsRequired();
            builder.Property(x => x.ShortDescription).HasMaxLength(500);
            builder.Property(x => x.DetailedDescription);
            builder.Property(x => x.ImageUrl).HasMaxLength(500);
            builder.Property(x => x.IconClass).HasMaxLength(100);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
