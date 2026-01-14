using Core.Abstract;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IBlogPostDAL : IBaseRepository<BlogPost>
    {
        List<BlogPost> GetByCategoryId(int categoryId);
        BlogPost GetBySlug(string slug);
        BlogPost GetByIdWithCategory(int id);
        List<BlogPost> GetAllWithCategory();
        List<BlogPost> GetPublishedPosts();
        List<BlogPost> GetRecentPosts(int count);
        void IncrementViewCount(int id);
        List<BlogPost> Search(string searchTerm);
    }
}
