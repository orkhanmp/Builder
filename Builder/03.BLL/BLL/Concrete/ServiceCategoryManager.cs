using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Business;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.DTOs.ContentDTO.ServiceCategoryDTOs;
using Entities.TableModels.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class ServiceCategoryManager : IServiceCategoryService
    {
        private readonly IServiceCategoryDAL _serviceCategoryDal;
        private readonly IMapper _mapper;

        public ServiceCategoryManager(IServiceCategoryDAL serviceCategoryDal, IMapper mapper)
        {
            _serviceCategoryDal = serviceCategoryDal;
            _mapper = mapper;
        }

        public IResult Add(CreateServiceCategoryDto createServiceCategoryDto)
        {
            var serviceCategoryMapper = _mapper.Map<ServiceCategory>(createServiceCategoryDto);
            var validateValidator = new ServiceCategoryValidation();
            var validationResult = validateValidator.Validate(serviceCategoryMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(DuplicateServiceCategoryName(serviceCategoryMapper));
            if (!checkData.Success)
                return checkData;

            serviceCategoryMapper.Deleted = 0;
            _serviceCategoryDal.Add(serviceCategoryMapper);
            return new SuccessResult("Service category added successfully");
        }

        public IResult Update(UpdateServiceCategoryDto updateServiceCategoryDto)
        {
            var serviceCategoryMapper = _mapper.Map<ServiceCategory>(updateServiceCategoryDto);
            var validateValidator = new ServiceCategoryValidation();
            var validationResult = validateValidator.Validate(serviceCategoryMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(DuplicateServiceCategoryName(serviceCategoryMapper));
            if (!checkData.Success)
                return checkData;

            _serviceCategoryDal.Update(serviceCategoryMapper);
            return new SuccessResult("Service category updated successfully");
        }

        public IResult Delete(int id)
        {
            var serviceCategoryGet = _serviceCategoryDal.Get(x => x.Id == id && x.Deleted == 0);
            if (serviceCategoryGet is not null)
            {
                serviceCategoryGet.Deleted = id;
                _serviceCategoryDal.Update(serviceCategoryGet);
                return new SuccessResult("Service category deleted successfully");
            }
            return new ErrorResult("Service category not found");
        }

        public IDataResult<ServiceCategoryDto> Get(int id)
        {
            var serviceCategory = _serviceCategoryDal.Get(x => x.Id == id && x.Deleted == 0);
            if (serviceCategory is null)
                return new ErrorDataResult<ServiceCategoryDto>("Service category not found");

            return new SuccessDataResult<ServiceCategoryDto>(_mapper.Map<ServiceCategoryDto>(serviceCategory));
        }

        public IDataResult<List<ServiceCategoryDto>> GetAll()
        {
            var serviceCategories = _serviceCategoryDal.GetAll(x => x.Deleted == 0);
            return new SuccessDataResult<List<ServiceCategoryDto>>(_mapper.Map<List<ServiceCategoryDto>>(serviceCategories));
        }

        public IDataResult<ServiceCategoryDetailDto> GetByIdWithServices(int id)
        {
            var serviceCategory = _serviceCategoryDal.GetByIdWithServices(id);

            if (serviceCategory is null)
                return new ErrorDataResult<ServiceCategoryDetailDto>("Service category not found");

            return new SuccessDataResult<ServiceCategoryDetailDto>(_mapper.Map<ServiceCategoryDetailDto>(serviceCategory));
        }

        public IDataResult<List<ServiceCategoryDetailDto>> GetAllWithServices()
        {
            var serviceCategories = _serviceCategoryDal.GetAllWithServices();
            return new SuccessDataResult<List<ServiceCategoryDetailDto>>(_mapper.Map<List<ServiceCategoryDetailDto>>(serviceCategories));
        }

        public IDataResult<List<ServiceCategoryDetailDto>> GetActiveWithServices()
        {
            var serviceCategories = _serviceCategoryDal.GetActiveWithServices();
            return new SuccessDataResult<List<ServiceCategoryDetailDto>>(_mapper.Map<List<ServiceCategoryDetailDto>>(serviceCategories));
        }

        private IResult DuplicateServiceCategoryName(ServiceCategory serviceCategory)
        {            
            var categoryName = _serviceCategoryDal.Get(x => x.Name == serviceCategory.Name && x.Id != serviceCategory.Id && x.Deleted == 0);
            if (categoryName is not null)
            {
                return new ErrorResult("Duplicate category name");
            }
            return new SuccessResult();
        }
    }
}
