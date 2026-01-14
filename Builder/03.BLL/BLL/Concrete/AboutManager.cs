using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.DTOs.ContentDTO.AboutDTOs;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{   
    public class AboutManager : IAboutService
    {
        private readonly IAboutDAL _aboutDal;
        private readonly IMapper _mapper;

        public AboutManager(IAboutDAL aboutDal, IMapper mapper)
        {
            _aboutDal = aboutDal;
            _mapper = mapper;
        }

        public IResult Add(CreateAboutDto createAboutDto)
        {
            var aboutMapper = _mapper.Map<About>(createAboutDto);
            var validateValidator = new AboutValidation();
            var validationResult = validateValidator.Validate(aboutMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            aboutMapper.Deleted = 0; 
            _aboutDal.Add(aboutMapper);
            return new SuccessResult("About added successfully");
        }

        public IResult Update(UpdateAboutDto updateAboutDto)
        {
            var aboutMapper = _mapper.Map<About>(updateAboutDto);
            var validateValidator = new AboutValidation();
            var validationResult = validateValidator.Validate(aboutMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            _aboutDal.Update(aboutMapper);
            return new SuccessResult("About updated successfully");
        }

        public IResult Delete(int id)
        {
            var aboutGet = _aboutDal.Get(x => x.Id == id && x.Deleted == 0);
            if (aboutGet is not null)
            {
                aboutGet.Deleted = id; 
                _aboutDal.Update(aboutGet); 
                return new SuccessResult("About deleted successfully");
            }
            return new ErrorResult("About not found");
        }

        public IDataResult<AboutDto> Get(int id)
        {
            var about = _aboutDal.Get(x => x.Id == id && x.Deleted == 0); 
            if (about is null)
                return new ErrorDataResult<AboutDto>("About not found");

            return new SuccessDataResult<AboutDto>(_mapper.Map<AboutDto>(about));
        }

        public IDataResult<List<AboutDto>> GetAll()
        {
            var abouts = _aboutDal.GetAll(x => x.Deleted == 0); 
            return new SuccessDataResult<List<AboutDto>>(_mapper.Map<List<AboutDto>>(abouts));
        }

        public IDataResult<AboutDto> GetCurrent()
        {
            
                var about = _aboutDal.GetCurrent();
            if (about is null)
                    return new ErrorDataResult<AboutDto>("About not found");

                return new SuccessDataResult<AboutDto>(_mapper.Map<AboutDto>(about));
            
        }
    }
}
