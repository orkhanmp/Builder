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
        private readonly ApplicationDbContext _context;
        public ServiceCategoryDAL(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public ServiceCategory GetByIdWithServices(int id)
        {
            return _context.ServiceCategories
                .Include(x => x.Services.Where(s => s.Deleted == 0))
                .FirstOrDefault(x => x.Id == id && x.Deleted == 0);
        }

        public List<ServiceCategory> GetAllWithServices()
        {
            return _context.ServiceCategories
                .Include(x => x.Services.Where(s => s.Deleted == 0))
                .Where(x => x.Deleted == 0)
                .ToList();
        }

        public List<ServiceCategory> GetActiveWithServices()
        {
            return _context.ServiceCategories
                .Include(x => x.Services.Where(s => s.IsActive && s.Deleted == 0))
                .Where(x => x.IsActive && x.Deleted == 0)
                .ToList();
        }
    }
}
