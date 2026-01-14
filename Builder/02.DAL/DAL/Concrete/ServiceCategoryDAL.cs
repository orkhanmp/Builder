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
    public class ServiceCategoryDAL : BaseRepository<ServiceCategory, ApplicationDbContext>, IServiceCategoryDAL
    {
        public ServiceCategory GetByIdWithServices(int id)
        {
            using (ApplicationDbContext context = new())
            {
                return context.ServiceCategories
                    .Include(x => x.Services)
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public List<ServiceCategory> GetAllWithServices()
        {
            using (ApplicationDbContext context = new())
            {
                return context.ServiceCategories
                    .Include(x => x.Services)
                    .ToList();
            }
        }

        public List<ServiceCategory> GetActiveWithServices()
        {
            using (ApplicationDbContext context = new())
            {
                return context.ServiceCategories
                    .Include(x => x.Services.Where(s => s.IsActive))
                    .Where(x => x.IsActive)
                    .ToList();
            }
        }
    }
}
