using Core.Result.Abstract;
using Entities.DTOs.ContentDTO.ServiceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IServiceService
    {
        IResult Add(CreateServiceDto createServiceDto);
        IResult Update(UpdateServiceDto updateServiceDto);
        IResult Delete(int id);
        IDataResult<List<ServiceDto>> GetAll();
        IDataResult<ServiceDto> Get(int id);
        IDataResult<List<ServiceDto>> GetByCategoryId(int categoryId);
        IDataResult<ServiceDetailDto> GetByIdWithCategory(int id);
        IDataResult<List<ServiceDto>> GetAllWithCategory();
        IDataResult<List<ServiceDto>> GetActiveServices();
    }
}
