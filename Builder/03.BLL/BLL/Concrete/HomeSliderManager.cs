using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.DTOs.ContentDTO.HomeSliderDTOs;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class HomeSliderManager : IHomeSliderService
    {
        private readonly IHomeSliderDAL _homeSliderDal;
        private readonly IMapper _mapper;

        public HomeSliderManager(IHomeSliderDAL homeSliderDal, IMapper mapper)
        {
            _homeSliderDal = homeSliderDal;
            _mapper = mapper;
        }

        public IResult Add(CreateHomeSliderDto createHomeSliderDto)
        {
            var homeSliderMapper = _mapper.Map<HomeSlider>(createHomeSliderDto);
            var validateValidator = new HomeSliderValidation();
            var validationResult = validateValidator.Validate(homeSliderMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            homeSliderMapper.Deleted = 0;
            _homeSliderDal.Add(homeSliderMapper);
            return new SuccessResult("Slider added successfully");
        }

        public IResult Update(UpdateHomeSliderDto updateHomeSliderDto)
        {
            var homeSliderMapper = _mapper.Map<HomeSlider>(updateHomeSliderDto);
            var validateValidator = new HomeSliderValidation();
            var validationResult = validateValidator.Validate(homeSliderMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            _homeSliderDal.Update(homeSliderMapper);
            return new SuccessResult("Slider updated successfully");
        }

        public IResult Delete(int id)
        {
            var homeSliderGet = _homeSliderDal.Get(x => x.Id == id && x.Deleted == 0);
            if (homeSliderGet is not null)
            {
                homeSliderGet.Deleted = id;
                _homeSliderDal.Update(homeSliderGet);
                return new SuccessResult("Slider deleted successfully");
            }
            return new ErrorResult("Slider not found");
        }

        public IDataResult<HomeSliderDto> Get(int id)
        {
            var homeSlider = _homeSliderDal.Get(x => x.Id == id && x.Deleted == 0);
            if (homeSlider is null)
                return new ErrorDataResult<HomeSliderDto>("Slider not found");

            return new SuccessDataResult<HomeSliderDto>(_mapper.Map<HomeSliderDto>(homeSlider));
        }

        public IDataResult<List<HomeSliderDto>> GetAll()
        {
            var homeSliders = _homeSliderDal.GetAll(x => x.Deleted == 0);
            return new SuccessDataResult<List<HomeSliderDto>>(_mapper.Map<List<HomeSliderDto>>(homeSliders));
        }

        public IDataResult<List<HomeSliderDto>> GetActiveSliders()
        {
            var homeSliders = _homeSliderDal.GetAll(x => x.IsActive && x.Deleted == 0);
            return new SuccessDataResult<List<HomeSliderDto>>(_mapper.Map<List<HomeSliderDto>>(homeSliders));
        }

        public IDataResult<List<HomeSliderDto>> GetByDisplayOrder()
        {
            using (ApplicationDbContext context = new())
            {
                var homeSliders = context.HomeSliders
                    .Where(x => x.IsActive && x.Deleted == 0)
                    .OrderBy(x => x.Order)
                    .ToList();
                return new SuccessDataResult<List<HomeSliderDto>>(_mapper.Map<List<HomeSliderDto>>(homeSliders));
            }
        }
    }
}
