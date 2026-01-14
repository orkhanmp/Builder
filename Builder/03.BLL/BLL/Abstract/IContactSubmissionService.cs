using Core.Result.Abstract;
using Entities.DTOs.ContentDTO.ContactSubmissionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IContactSubmissionService
    {
        IResult Add(CreateContactSubmissionDto createContactSubmissionDto);
        IResult Update(UpdateContactSubmissionDto updateContactSubmissionDto);
        IResult Delete(int id);
        IDataResult<List<ContactSubmissionDto>> GetAll();
        IDataResult<ContactSubmissionDto> Get(int id);
        IDataResult<List<ContactSubmissionDto>> GetUnread();
        IDataResult<List<ContactSubmissionDto>> GetByDateRange(DateTime startDate, DateTime endDate);
        IResult MarkAsRead(int id);
        IResult MarkAsReplied(int id);
    }
}
