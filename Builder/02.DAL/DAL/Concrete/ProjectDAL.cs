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
        public List<Project> GetByCategoryId(int categoryId)
        {
            return GetAll(x => x.ProjectCategoryId == categoryId);
        }

        public List<Project> GetByStatus(string status)
        {
            return GetAll(x => x.Status == status);
        }

        public List<Project> GetFeaturedProjects()
        {
            return GetAll(x => x.IsFeatured && x.IsActive);
        }

        public Project GetByIdWithCategory(int id)
        {
            using (ApplicationDbContext context = new())
            {
                return context.Projects
                    .Include(x => x.ProjectCategory)
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Project> GetAllWithCategory()
        {
            using (ApplicationDbContext context = new())
            {
                return context.Projects
                    .Include(x => x.ProjectCategory)
                    .ToList();
            }
        }
    }

}
