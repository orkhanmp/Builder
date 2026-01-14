using Core.Result.Abstract;
using Entities.DTOs.ContentDTO.HomeSliderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IHomeSliderService
    {
        IResult Add(CreateHomeSliderDto createHomeSliderDto);
        IResult Update(UpdateHomeSliderDto updateHomeSliderDto);
        IResult Delete(int id);
        IDataResult<List<HomeSliderDto>> GetAll();
        IDataResult<HomeSliderDto> Get(int id);
        IDataResult<List<HomeSliderDto>> GetActiveSliders();
        IDataResult<List<HomeSliderDto>> GetByDisplayOrder();
    }
}
