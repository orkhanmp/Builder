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
    public class ProjectDAL : BaseRepository<Project, ApplicationDbContext>, IProjectDAL
    {
        private readonly ApplicationDbContext _context;
        public ProjectDAL(ApplicationDbContext _context) : base(_context)
        {
            this._context = _context;
        }
        public List<Project> GetByCategoryId(int categoryId)
        {
            return GetAll(x => x.ProjectCategoryId == categoryId && x.Deleted == 0);
        }

        public List<Project> GetByStatus(string status)
        {
            return GetAll(x => x.Status == status && x.Deleted == 0);
        }

        public List<Project> GetFeaturedProjects()
        {
            return GetAll(x => x.IsFeatured && x.IsActive && x.Deleted == 0);
        }

        public Project GetByIdWithCategory(int id)
        {
            
                return _context.Projects
                    .Include(x => x.ProjectCategory)
                    .FirstOrDefault(x => x.Id == id && x.Deleted == 0);

        }

        public List<Project> GetAllWithCategory()
        {

            return _context.Projects
            .Include(x => x.ProjectCategory)
            .Where(x => x.Deleted == 0)
            .ToList();

        }
    }

}
