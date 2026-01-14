using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Business;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using Entities.DTOs.ContentDTO.PageBannerDTOs;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class PageBannerManager : IPageBannerService
    {
        private readonly IPageBannerDAL _pageBannerDal;
        private readonly IMapper _mapper;
        public PageBannerManager(IPageBannerDAL pageBannerDal, IMapper mapper)
        {
            _pageBannerDal = pageBannerDal;
            _mapper = mapper;
        }

        public IResult Add(CreatePageBannerDto createPageBannerDto)
        {
            var pageBannerMapper = _mapper.Map<PageBanner>(createPageBannerDto);
            var validateValidator = new PageBannerValidation();
            var validationResult = validateValidator.Validate(pageBannerMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(DuplicatePageName(pageBannerMapper));
            if (!checkData.Success)
                return checkData;

            pageBannerMapper.Deleted = 0;
            _pageBannerDal.Add(pageBannerMapper);
            return new SuccessResult("Page banner added successfully");
        }

        public IResult Update(UpdatePageBannerDto updatePageBannerDto)
        {
            var pageBannerMapper = _mapper.Map<PageBanner>(updatePageBannerDto);
            var validateValidator = new PageBannerValidation();
            var validationResult = validateValidator.Validate(pageBannerMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(DuplicatePageName(pageBannerMapper));
            if (!checkData.Success)
                return checkData;

            _pageBannerDal.Update(pageBannerMapper);
            return new SuccessResult("Page banner updated successfully");
        }

        public IResult Delete(int id)
        {
            var pageBannerGet = _pageBannerDal.Get(x => x.Id == id && x.Deleted == 0);
            if (pageBannerGet is not null)
            {
                pageBannerGet.Deleted = id;
                _pageBannerDal.Update(pageBannerGet);
                return new SuccessResult("Page banner deleted successfully");
            }
            return new ErrorResult("Page banner not found");
        }

        public IDataResult<PageBannerDto> Get(int id)
        {
            var pageBanner = _pageBannerDal.Get(x => x.Id == id && x.Deleted == 0);
            if (pageBanner is null)
                return new ErrorDataResult<PageBannerDto>("Page banner not found");

            return new SuccessDataResult<PageBannerDto>(_mapper.Map<PageBannerDto>(pageBanner));
        }

        public IDataResult<List<PageBannerDto>> GetAll()
        {
            var pageBanners = _pageBannerDal.GetAll(x => x.Deleted == 0);
            return new SuccessDataResult<List<PageBannerDto>>(_mapper.Map<List<PageBannerDto>>(pageBanners));
        }

        public IDataResult<PageBannerDto> GetByPageName(string pageName)
        {
            var pageBanner = _pageBannerDal.Get(x => x.PageName == pageName && x.IsActive && x.Deleted == 0);
            if (pageBanner is null)
                return new ErrorDataResult<PageBannerDto>("Page banner not found");

            return new SuccessDataResult<PageBannerDto>(_mapper.Map<PageBannerDto>(pageBanner));
        }

        private IResult DuplicatePageName(PageBanner pageBanner)
        {
            var pageName = _pageBannerDal.Get(x => x.PageName == pageBanner.PageName && x.Id != pageBanner.Id && x.Deleted == 0);
            if (pageName is not null)
            {
                return new ErrorResult("Page banner already exists for this page");
            }
            return new SuccessResult();
        }
    }
}
