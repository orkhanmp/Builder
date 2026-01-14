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
    public class TeamCategoryConfiguration : IEntityTypeConfiguration<TeamCategory>
    {
        public void Configure(EntityTypeBuilder<TeamCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasMany(x => x.TeamMembers)
                   .WithOne(x => x.TeamCategory)
                   .HasForeignKey(x => x.TeamCategoryId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
