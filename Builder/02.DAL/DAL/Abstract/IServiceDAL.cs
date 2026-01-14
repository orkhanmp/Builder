using Core.Abstract;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IServiceDAL: IBaseRepository<Service>
    {
        List<Service> GetByCategoryId(int categoryId);
        Service GetByIdWithCategory(int id);
        List<Service> GetAllWithCategory();
        List<Service> GetActiveServices();
    }
}
