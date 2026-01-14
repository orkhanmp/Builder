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
    public class BlogPostDAL : BaseRepository<BlogPost, ApplicationDbContext>, IBlogPostDAL
    {
        public List<BlogPost> GetByCategoryId(int categoryId)
        {
            return GetAll(x => x.BlogCategoryId == categoryId);
        }

        public BlogPost GetBySlug(string slug)
        {
            return Get(x => x.Slug == slug);
        }

        public BlogPost GetByIdWithCategory(int id)
        {
            using (ApplicationDbContext context = new())
            {
                return context.BlogPosts
                    .Include(x => x.BlogCategory)
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public List<BlogPost> GetAllWithCategory()
        {
            using (ApplicationDbContext context = new())
            {
                return context.BlogPosts
                    .Include(x => x.BlogCategory)
                    .OrderByDescending(x => x.PublishDate)
                    .ToList();
            }
        }

        public List<BlogPost> GetPublishedPosts()
        {
            return GetAll(x => x.IsPublished && x.PublishDate <= DateTime.Now);
        }

        public List<BlogPost> GetRecentPosts(int count)
        {
            using (ApplicationDbContext context = new())
            {
                return context.BlogPosts
                    .Where(x => x.IsPublished)
                    .OrderByDescending(x => x.PublishDate)
                    .Take(count)
                    .ToList();
            }
        }

        public void IncrementViewCount(int id)
        {
            using (ApplicationDbContext context = new())
            {
                var post = context.BlogPosts.Find(id);
                if (post != null)
                {
                    post.ViewCount++;
                    context.SaveChanges();
                }
            }
        }

        public List<BlogPost> Search(string searchTerm)
        {
            using (ApplicationDbContext context = new())
            {
                return context.BlogPosts
                    .Where(x => x.IsPublished &&
                        (x.Title.Contains(searchTerm) || x.Summary.Contains(searchTerm)))
                    .OrderByDescending(x => x.PublishDate)
                    .ToList();
            }
        }
    }
}
