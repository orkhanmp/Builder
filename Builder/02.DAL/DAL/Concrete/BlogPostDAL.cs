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
        private readonly ApplicationDbContext _context;
        public BlogPostDAL(ApplicationDbContext _context) : base(_context)
        {
            this._context = _context;
        }
        public List<BlogPost> GetByCategoryId(int categoryId)
        {
            return GetAll(x => x.BlogCategoryId == categoryId);
        }

        public BlogPost GetBySlug(string slug)
        {
            return _context.BlogPosts
                .Include(x => x.BlogCategory)
                .FirstOrDefault(x => x.Slug == slug && x.Deleted == 0);
        }

        public BlogPost GetByIdWithCategory(int id)
        {

            return _context.BlogPosts
            .Include(x => x.BlogCategory)
            .FirstOrDefault(x => x.Id == id && x.Deleted == 0);

        }

        public List<BlogPost> GetAllWithCategory()
        {

            return _context.BlogPosts
            .Include(x => x.BlogCategory)
            .Where(x => x.Deleted == 0)
            .OrderByDescending(x => x.PublishDate)
            .ToList();
        }

        public List<BlogPost> GetPublishedPosts()
        {
            return GetAll(x => x.IsPublished && x.PublishDate <= DateTime.Now);
        }

        public List<BlogPost> GetRecentPosts(int count)
        {

            return _context.BlogPosts
            .Where(x => x.IsPublished && x.Deleted == 0)
            .OrderByDescending(x => x.PublishDate)
            .Take(count)
            .ToList();  
        }

        public void IncrementViewCount(int id)
        {

            var post = _context.BlogPosts.FirstOrDefault(x => x.Id == id && x.Deleted == 0);
            if (post != null)
            {
                post.ViewCount++;
                _context.SaveChanges();
            }

        }

        public List<BlogPost> Search(string searchTerm)
        {

            return _context.BlogPosts
            .Where(x => x.IsPublished && x.Deleted == 0 &&
                (x.Title.Contains(searchTerm) || x.Summary.Contains(searchTerm)))
            .OrderByDescending(x => x.PublishDate)
            .ToList();

        }
    }
}
