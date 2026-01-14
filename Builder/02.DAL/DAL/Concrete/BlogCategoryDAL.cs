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
    public class BlogCategoryDAL : BaseRepository<BlogCategory, ApplicationDbContext>, IBlogCategoryDAL
    {
        public BlogCategory GetByIdWithPosts(int id)
        {
            using (ApplicationDbContext context = new())
            {
                return context.BlogCategories
                    .Include(x => x.BlogPosts)
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public BlogCategory GetBySlugWithPosts(string slug)
        {
            using (ApplicationDbContext context = new())
            {
                return context.BlogCategories
                    .Include(x => x.BlogPosts.Where(p => p.IsPublished))
                    .FirstOrDefault(x => x.Slug == slug);
            }
        }

        public List<BlogCategory> GetAllWithPosts()
        {
            using (ApplicationDbContext context = new())
            {
                return context.BlogCategories
                    .Include(x => x.BlogPosts)
                    .ToList();
            }
        }

        public List<BlogCategory> GetActiveWithPosts()
        {
            using (ApplicationDbContext context = new())
            {
                return context.BlogCategories
                    .Include(x => x.BlogPosts.Where(p => p.IsPublished))
                    .Where(x => x.IsActive)
                    .ToList();
            }
        }
    }
}
