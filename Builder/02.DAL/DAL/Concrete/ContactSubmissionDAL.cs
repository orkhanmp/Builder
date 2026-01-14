using Core.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class ContactSubmissionDAL : BaseRepository<ContactSubmission, ApplicationDbContext>, IContactSubmissionDAL
    {
        public List<ContactSubmission> GetUnread()
        {
            return GetAll(x => !x.IsRead);
        }

        public List<ContactSubmission> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            using (ApplicationDbContext context = new())
            {
                return context.ContactSubmissions
                    .Where(x => x.SubmittedDate >= startDate && x.SubmittedDate <= endDate)
                    .OrderByDescending(x => x.SubmittedDate)
                    .ToList();
            }
        }

        public void MarkAsRead(int id)
        {
            using (ApplicationDbContext context = new())
            {
                var submission = context.ContactSubmissions.Find(id);
                if (submission != null)
                {
                    submission.IsRead = true;
                    context.SaveChanges();
                }
            }
        }

        public void MarkAsReplied(int id)
        {
            using (ApplicationDbContext context = new())
            {
                var submission = context.ContactSubmissions.Find(id);
                if (submission != null)
                {
                    submission.IsReplied = true;
                    submission.IsRead = true;
                    context.SaveChanges();
                }
            }
        }
    }
}
