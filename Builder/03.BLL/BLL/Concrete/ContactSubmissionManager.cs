using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.DTOs.ContentDTO.ContactSubmissionDTOs;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class ContactSubmissionManager : IContactSubmissionService
    {
        private readonly IContactSubmissionDAL _contactSubmissionDal;
        private readonly IMapper _mapper;
        public ContactSubmissionManager(IContactSubmissionDAL contactSubmissionDal, IMapper mapper)
        {
            _contactSubmissionDal = contactSubmissionDal;
            _mapper = mapper;
        }

        public IResult Add(CreateContactSubmissionDto createContactSubmissionDto)
        {
            var contactSubmissionMapper = _mapper.Map<ContactSubmission>(createContactSubmissionDto);
            var validateValidator = new ContactSubmissionValidation();
            var validationResult = validateValidator.Validate(contactSubmissionMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            contactSubmissionMapper.Deleted = 0;
            _contactSubmissionDal.Add(contactSubmissionMapper);
            return new SuccessResult("Your message has been sent successfully");
        }

        public IResult Update(UpdateContactSubmissionDto updateContactSubmissionDto)
        {
            var contactSubmissionMapper = _mapper.Map<ContactSubmission>(updateContactSubmissionDto);
            _contactSubmissionDal.Update(contactSubmissionMapper);
            return new SuccessResult("Contact submission updated successfully");
        }

        public IResult Delete(int id)
        {
            var contactSubmissionGet = _contactSubmissionDal.Get(x => x.Id == id && x.Deleted == 0);
            if (contactSubmissionGet is not null)
            {
                contactSubmissionGet.Deleted = id;
                _contactSubmissionDal.Update(contactSubmissionGet);
                return new SuccessResult("Contact submission deleted successfully");
            }
            return new ErrorResult("Contact submission not found");
        }

        public IDataResult<ContactSubmissionDto> Get(int id)
        {
            var contactSubmission = _contactSubmissionDal.Get(x => x.Id == id && x.Deleted == 0);
            if (contactSubmission is null)
                return new ErrorDataResult<ContactSubmissionDto>("Contact submission not found");

            return new SuccessDataResult<ContactSubmissionDto>(_mapper.Map<ContactSubmissionDto>(contactSubmission));
        }

        public IDataResult<List<ContactSubmissionDto>> GetAll()
        {
            var contactSubmissions = _contactSubmissionDal.GetAll(x => x.Deleted == 0);
            return new SuccessDataResult<List<ContactSubmissionDto>>(_mapper.Map<List<ContactSubmissionDto>>(contactSubmissions));
        }

        public IDataResult<List<ContactSubmissionDto>> GetUnread()
        {
            var contactSubmissions = _contactSubmissionDal.GetAll(x => !x.IsRead && x.Deleted == 0);
            return new SuccessDataResult<List<ContactSubmissionDto>>(_mapper.Map<List<ContactSubmissionDto>>(contactSubmissions));
        }

        public IDataResult<List<ContactSubmissionDto>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            using (ApplicationDbContext context = new())
            {
                var contactSubmissions = context.ContactSubmissions
                    .Where(x => x.SubmittedDate >= startDate && x.SubmittedDate <= endDate && x.Deleted == 0)
                    .OrderByDescending(x => x.SubmittedDate)
                    .ToList();

                return new SuccessDataResult<List<ContactSubmissionDto>>(_mapper.Map<List<ContactSubmissionDto>>(contactSubmissions));
            }
        }

        public IResult MarkAsRead(int id)
        {
            var contactSubmission = _contactSubmissionDal.Get(x => x.Id == id && x.Deleted == 0);
            if (contactSubmission is null)
                return new ErrorResult("Contact submission not found");

            using (ApplicationDbContext context = new())
            {
                var submission = context.ContactSubmissions.Find(id);
                if (submission != null)
                {
                    submission.IsRead = true;
                    context.SaveChanges();
                }
            }

            return new SuccessResult("Marked as read");
        }

        public IResult MarkAsReplied(int id)
        {
            var contactSubmission = _contactSubmissionDal.Get(x => x.Id == id && x.Deleted == 0);
            if (contactSubmission is null)
                return new ErrorResult("Contact submission not found");

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

            return new SuccessResult("Marked as replied");
        }
    }
}
