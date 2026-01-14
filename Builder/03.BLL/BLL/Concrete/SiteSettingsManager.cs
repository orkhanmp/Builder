using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.DTOs.ContentDTO.SiteSettingsDTOs;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class SiteSettingsManager : ISiteSettingsService
    {
        private readonly ISiteSettingsDAL _siteSettingsDal;
        private readonly IMapper _mapper;
        public SiteSettingsManager(ISiteSettingsDAL siteSettingsDal, IMapper mapper)
        {
            _siteSettingsDal = siteSettingsDal;
            _mapper = mapper;
        }

        public IResult Update(UpdateSiteSettingsDto updateSiteSettingsDto)
        {
            var siteSettingsMapper = _mapper.Map<SiteSettings>(updateSiteSettingsDto);
            var validateValidator = new SiteSettingsValidation();
            var validationResult = validateValidator.Validate(siteSettingsMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            _siteSettingsDal.Update(siteSettingsMapper);
            return new SuccessResult("Site settings updated successfully");
        }

        public IDataResult<SiteSettingsDto> Get(int id)
        {
            var siteSettings = _siteSettingsDal.Get(x => x.Id == id && x.Deleted == 0);
            if (siteSettings is null)
                return new ErrorDataResult<SiteSettingsDto>("Site settings not found");

            return new SuccessDataResult<SiteSettingsDto>(_mapper.Map<SiteSettingsDto>(siteSettings));
        }

        public IDataResult<SiteSettingsDto> GetCurrentSettings()
        {
            using (ApplicationDbContext context = new())
            {
                var siteSettings = context.SiteSettings.FirstOrDefault(x => x.Deleted == 0);
                if (siteSettings is null)
                    return new ErrorDataResult<SiteSettingsDto>("Site settings not found");

                return new SuccessDataResult<SiteSettingsDto>(_mapper.Map<SiteSettingsDto>(siteSettings));
            }
        }
    }
}
