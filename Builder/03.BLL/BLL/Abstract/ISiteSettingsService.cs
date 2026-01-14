using Core.Result.Abstract;
using Entities.DTOs.ContentDTO.SiteSettingsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ISiteSettingsService
    {
        IResult Update(UpdateSiteSettingsDto updateSiteSettingsDto);
        IDataResult<SiteSettingsDto> Get(int id);
        IDataResult<SiteSettingsDto> GetCurrentSettings();
    }
}
