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
    public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.FullName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Position).HasMaxLength(200);
            builder.Property(x => x.Bio).HasMaxLength(2000);
            builder.Property(x => x.ImageUrl).HasMaxLength(500);
            builder.Property(x => x.Email).HasMaxLength(200);
            builder.Property(x => x.Phone).HasMaxLength(50);
            builder.Property(x => x.FacebookUrl).HasMaxLength(500);
            builder.Property(x => x.TwitterUrl).HasMaxLength(500);
            builder.Property(x => x.LinkedInUrl).HasMaxLength(500);
            builder.Property(x => x.InstagramUrl).HasMaxLength(500);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
