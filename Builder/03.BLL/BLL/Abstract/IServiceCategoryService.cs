using Core.Result.Abstract;
using Entities.DTOs.ContentDTO.ServiceCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IServiceCategoryService
    {
        IResult Add(CreateServiceCategoryDto createServiceCategoryDto);
        IResult Update(UpdateServiceCategoryDto updateServiceCategoryDto);
        IResult Delete(int id);
        IDataResult<List<ServiceCategoryDto>> GetAll();
        IDataResult<ServiceCategoryDto> Get(int id);
        IDataResult<ServiceCategoryDetailDto> GetByIdWithServices(int id);
        IDataResult<List<ServiceCategoryDetailDto>> GetAllWithServices();
        IDataResult<List<ServiceCategoryDetailDto>> GetActiveWithServices();
    }
}
