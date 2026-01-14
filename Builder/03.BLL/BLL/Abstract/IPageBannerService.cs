using Core.Result.Abstract;
using Entities.DTOs.ContentDTO.PageBannerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IPageBannerService
    {
        IResult Add(CreatePageBannerDto createPageBannerDto);
        IResult Update(UpdatePageBannerDto updatePageBannerDto);
        IResult Delete(int id);
        IDataResult<List<PageBannerDto>> GetAll();
        IDataResult<PageBannerDto> Get(int id);
        IDataResult<PageBannerDto> GetByPageName(string pageName);
    }
}
