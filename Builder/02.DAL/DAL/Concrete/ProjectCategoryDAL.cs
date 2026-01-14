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
        public ProjectCategory GetByIdWithProjects(int id)
        {
            using (ApplicationDbContext context = new())
            {
                return context.ProjectCategories
                    .Include(x => x.Projects)
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public List<ProjectCategory> GetAllWithProjects()
        {
            using (ApplicationDbContext context = new())
            {
                return context.ProjectCategories
                    .Include(x => x.Projects)
                    .ToList();
            }
        }

        public List<ProjectCategory> GetActiveWithProjects()
        {
            using (ApplicationDbContext context = new())
            {
                return context.ProjectCategories
                    .Include(x => x.Projects.Where(p => p.IsActive))
                    .Where(x => x.IsActive)
                    .ToList();
            }
        }
    }
}
