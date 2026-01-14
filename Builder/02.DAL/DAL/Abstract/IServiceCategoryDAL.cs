using Core.Abstract;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IServiceCategoryDAL: IBaseRepository<ServiceCategory>
    {
        ServiceCategory GetByIdWithServices(int id);
        List<ServiceCategory> GetAllWithServices();
        List<ServiceCategory> GetActiveWithServices();
    }
}
