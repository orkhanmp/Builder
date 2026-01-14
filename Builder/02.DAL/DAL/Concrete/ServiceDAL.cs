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
        private readonly ApplicationDbContext _context;
        public ServiceDAL(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Service> GetByCategoryId(int categoryId)
        {
            return GetAll(x => x.ServiceCategoryId == categoryId && x.Deleted == 0);
        }

        public Service GetByIdWithCategory(int id)
        {
            return _context.Services
                .Include(x => x.ServiceCategory)
                .FirstOrDefault(x => x.Id == id && x.Deleted == 0);
        }

        public List<Service> GetAllWithCategory()
        {
            return _context.Services
                .Include(x => x.ServiceCategory)
                .Where(x => x.Deleted == 0)
                .ToList();
        }

        public List<Service> GetActiveServices()
        {
            return GetAll(x => x.IsActive && x.Deleted == 0);
        }
    }
}
