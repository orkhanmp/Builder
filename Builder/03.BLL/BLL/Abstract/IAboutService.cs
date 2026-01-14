using Core.Result.Abstract;
using Entities.DTOs.ContentDTO.AboutDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IAboutService
    {
        IResult Add(CreateAboutDto createAboutDto);
        IResult Update(UpdateAboutDto updateAboutDto);
        IResult Delete(int id);
        IDataResult<List<AboutDto>> GetAll();
        IDataResult<AboutDto> Get(int id);
        IDataResult<AboutDto> GetCurrent();
    }
}
