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
    public class ProjectCategoryDAL : BaseRepository<ProjectCategory, ApplicationDbContext>, IProjectCategoryDAL
    {
        private readonly ApplicationDbContext _context;
        public ProjectCategoryDAL(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public ProjectCategory GetByIdWithProjects(int id)
        {
            return _context.ProjectCategories
                .Include(x => x.Projects.Where(p => p.Deleted == 0))
                .FirstOrDefault(x => x.Id == id && x.Deleted == 0);
        }

        public List<ProjectCategory> GetAllWithProjects()
        {
            return _context.ProjectCategories
                .Include(x => x.Projects.Where(p => p.Deleted == 0))
                .Where(x => x.Deleted == 0)
                .ToList();
        }

        public List<ProjectCategory> GetActiveWithProjects()
        {
            return _context.ProjectCategories
                .Include(x => x.Projects.Where(p => p.IsActive && p.Deleted == 0))
                .Where(x => x.IsActive && x.Deleted == 0)
                .ToList();
        }
    }
}
