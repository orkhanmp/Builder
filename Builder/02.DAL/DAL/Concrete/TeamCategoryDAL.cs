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
    public class TeamCategoryDAL : BaseRepository<TeamCategory, ApplicationDbContext>, ITeamCategoryDAL
    {
        private readonly ApplicationDbContext _context;
        public TeamCategoryDAL(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public TeamCategory GetByIdWithMembers(int id)
        {
            return _context.TeamCategories
                .Include(x => x.TeamMembers.Where(m => m.Deleted == 0))
                .FirstOrDefault(x => x.Id == id && x.Deleted == 0);
        }

        public List<TeamCategory> GetAllWithMembers()
        {
            return _context.TeamCategories
                .Include(x => x.TeamMembers.Where(m => m.Deleted == 0))
                .Where(x => x.Deleted == 0)
                .ToList();
        }

        public List<TeamCategory> GetActiveWithMembers()
        {
            return _context.TeamCategories
                .Include(x => x.TeamMembers.Where(m => m.IsActive && m.Deleted == 0))
                .Where(x => x.IsActive && x.Deleted == 0)
                .ToList();
        }
    }
}
