using Core.Abstract;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IContactSubmissionDAL : IBaseRepository<ContactSubmission>
    {
        List<ContactSubmission> GetUnread();
        List<ContactSubmission> GetByDateRange(DateTime startDate, DateTime endDate);
        void MarkAsRead(int id);
        void MarkAsReplied(int id);
    }
}
