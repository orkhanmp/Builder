using Core.Abstract;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IProjectCategoryDAL : IBaseRepository<ProjectCategory>
    {
        ProjectCategory GetByIdWithProjects(int id);
        List<ProjectCategory> GetAllWithProjects();
        List<ProjectCategory> GetActiveWithProjects();
    }
}
