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
        
        private readonly ApplicationDbContext _context;
        public BlogCategoryDAL(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public BlogCategory GetByIdWithPosts(int id)
        {
            return _context.BlogCategories
                .Include(x => x.BlogPosts.Where(p => p.Deleted == 0))
                .FirstOrDefault(x => x.Id == id && x.Deleted == 0);
        }

        public BlogCategory GetBySlugWithPosts(string slug)
        {
            return _context.BlogCategories
                .Include(x => x.BlogPosts.Where(p => p.IsPublished && p.Deleted == 0))
                .FirstOrDefault(x => x.Slug == slug && x.Deleted == 0);
        }

        public List<BlogCategory> GetAllWithPosts()
        {
            return _context.BlogCategories
                .Include(x => x.BlogPosts.Where(p => p.Deleted == 0))
                .Where(x => x.Deleted == 0)
                .ToList();
        }

        public List<BlogCategory> GetActiveWithPosts()
        {
            return _context.BlogCategories
                .Include(x => x.BlogPosts.Where(p => p.IsPublished && p.Deleted == 0))
                .Where(x => x.IsActive && x.Deleted == 0)
                .ToList();
        }
    }
}
