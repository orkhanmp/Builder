using Core.Abstract;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IBlogCategoryDAL : IBaseRepository<BlogCategory>
    {
        BlogCategory GetByIdWithPosts(int id);
        BlogCategory GetBySlugWithPosts(string slug);
        List<BlogCategory> GetAllWithPosts();
        List<BlogCategory> GetActiveWithPosts();
    }
}
