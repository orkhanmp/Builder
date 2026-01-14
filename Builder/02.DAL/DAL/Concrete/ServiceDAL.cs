using Core.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.TableModels.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class ServiceDAL : BaseRepository<Service, ApplicationDbContext>, IServiceDAL
    {
        public List<Service> GetByCategoryId(int categoryId)
        {
            return GetAll(x => x.ServiceCategoryId == categoryId);
        }

        public Service GetByIdWithCategory(int id)
        {
            using (ApplicationDbContext context = new())
            {
                return context.Services
                    .Include(x => x.ServiceCategory)
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Service> GetAllWithCategory()
        {
            using (ApplicationDbContext context = new())
            {
                return context.Services
                    .Include(x => x.ServiceCategory)
                    .ToList();
            }
        }

        public List<Service> GetActiveServices()
        {
            return GetAll(x => x.IsActive);
        }
    }
}
