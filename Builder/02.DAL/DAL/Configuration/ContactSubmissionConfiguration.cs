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
    public class ContactSubmissionConfiguration : IEntityTypeConfiguration<ContactSubmission>
    {
        public void Configure(EntityTypeBuilder<ContactSubmission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.FullName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Subject).HasMaxLength(300);
            builder.Property(x => x.Message).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.IpAddress).HasMaxLength(50);
            builder.Property(x => x.UserAgent).HasMaxLength(500);
            builder.Property(x => x.SubmittedDate).HasDefaultValueSql("GETDATE()");
        }
    }
}
