using Core.Abstract;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IProjectDAL : IBaseRepository<Project>
    {
        List<Project> GetByCategoryId(int categoryId);
        List<Project> GetByStatus(string status);
        List<Project> GetFeaturedProjects();
        Project GetByIdWithCategory(int id);
        List<Project> GetAllWithCategory();
    }
}
