using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Business;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.DTOs.ContentDTO.ServiceDTOs;
using Entities.TableModels.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class ServiceManager : IServiceService
    {
        private readonly IServiceDAL _serviceDal;
        private readonly IMapper _mapper;

        public ServiceManager(IServiceDAL serviceDal, IMapper mapper)
        {
            _serviceDal = serviceDal;
            _mapper = mapper;
        }

        public IResult Add(CreateServiceDto createServiceDto)
        {
            var serviceMapper = _mapper.Map<Service>(createServiceDto);
            var validateValidator = new ServiceValidation();
            var validationResult = validateValidator.Validate(serviceMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(DuplicateServiceTitle(serviceMapper));
            if (!checkData.Success)
                return checkData;

            serviceMapper.Deleted = 0;
            _serviceDal.Add(serviceMapper);
            return new SuccessResult("Service added successfully");
        }

        public IResult Update(UpdateServiceDto updateServiceDto)
        {
            var serviceMapper = _mapper.Map<Service>(updateServiceDto);
            var validateValidator = new ServiceValidation();
            var validationResult = validateValidator.Validate(serviceMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(DuplicateServiceTitle(serviceMapper));
            if (!checkData.Success)
                return checkData;

            _serviceDal.Update(serviceMapper);
            return new SuccessResult("Service updated successfully");
        }

        public IResult Delete(int id)
        {
            var serviceGet = _serviceDal.Get(x => x.Id == id && x.Deleted == 0);
            if (serviceGet is not null)
            {
                serviceGet.Deleted = id;
                _serviceDal.Update(serviceGet);
                return new SuccessResult("Service deleted successfully");
            }
            return new ErrorResult("Service not found");
        }

        public IDataResult<ServiceDto> Get(int id)
        {
            var service = _serviceDal.Get(x => x.Id == id && x.Deleted == 0);
            if (service is null)
                return new ErrorDataResult<ServiceDto>("Service not found");

            return new SuccessDataResult<ServiceDto>(_mapper.Map<ServiceDto>(service));
        }

        public IDataResult<List<ServiceDto>> GetAll()
        {
            var services = _serviceDal.GetAll(x => x.Deleted == 0);
            return new SuccessDataResult<List<ServiceDto>>(_mapper.Map<List<ServiceDto>>(services));
        }

        public IDataResult<List<ServiceDto>> GetByCategoryId(int categoryId)
        {
            var services = _serviceDal.GetAll(x => x.ServiceCategoryId == categoryId && x.Deleted == 0);
            return new SuccessDataResult<List<ServiceDto>>(_mapper.Map<List<ServiceDto>>(services));
        }

        public IDataResult<ServiceDetailDto> GetByIdWithCategory(int id)
        {
            using (ApplicationDbContext context = new())
            {
                var service = context.Services
                    .Include(x => x.ServiceCategory)
                    .FirstOrDefault(x => x.Id == id && x.Deleted == 0);

                if (service is null)
                    return new ErrorDataResult<ServiceDetailDto>("Service not found");

                return new SuccessDataResult<ServiceDetailDto>(_mapper.Map<ServiceDetailDto>(service));
            }
        }

        public IDataResult<List<ServiceDto>> GetAllWithCategory()
        {
            using (ApplicationDbContext context = new())
            {
                var services = context.Services
                    .Include(x => x.ServiceCategory)
                    .Where(x => x.Deleted == 0)
                    .ToList();

                return new SuccessDataResult<List<ServiceDto>>(_mapper.Map<List<ServiceDto>>(services));
            }
        }

        public IDataResult<List<ServiceDto>> GetActiveServices()
        {
            var services = _serviceDal.GetAll(x => x.IsActive && x.Deleted == 0);
            return new SuccessDataResult<List<ServiceDto>>(_mapper.Map<List<ServiceDto>>(services));
        }

        private IResult DuplicateServiceTitle(Service service)
        {
            var serviceTitle = _serviceDal.Get(x => x.Title == service.Title && x.Id != service.Id && x.IsActive && x.Deleted == 0);
            if (serviceTitle is not null)
            {
                return new ErrorResult("Duplicate service title");
            }
            return new SuccessResult();
        }
    }
}
