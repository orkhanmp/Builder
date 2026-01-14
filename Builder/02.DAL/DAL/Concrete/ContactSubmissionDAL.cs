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
        private readonly ApplicationDbContext _context;
        public ContactSubmissionDAL(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<ContactSubmission> GetUnread()
        {
            
            return GetAll(x => !x.IsRead && x.Deleted == 0);
        }

        public List<ContactSubmission> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.ContactSubmissions
                .Where(x => x.SubmittedDate >= startDate && x.SubmittedDate <= endDate && x.Deleted == 0)
                .OrderByDescending(x => x.SubmittedDate)
                .ToList();
        }

        public void MarkAsRead(int id)
        {
            var submission = _context.ContactSubmissions.FirstOrDefault(x => x.Id == id && x.Deleted == 0);
            if (submission != null)
            {
                submission.IsRead = true;
                _context.SaveChanges();
            }
        }

        public void MarkAsReplied(int id)
        {
            var submission = _context.ContactSubmissions.FirstOrDefault(x => x.Id == id && x.Deleted == 0);
            if (submission != null)
            {
                submission.IsReplied = true;
                submission.IsRead = true;
                _context.SaveChanges();
            }
        }
    }
}