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
    public class TeamMemberDAL : BaseRepository<TeamMember, ApplicationDbContext>, ITeamMemberDAL
    {
        private readonly ApplicationDbContext _context;
        public TeamMemberDAL(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<TeamMember> GetByCategoryId(int categoryId)
        {
            return GetAll(x => x.TeamCategoryId == categoryId && x.Deleted == 0);
        }

        public TeamMember GetByIdWithCategory(int id)
        {
            return _context.TeamMembers
                .Include(x => x.TeamCategory)
                .FirstOrDefault(x => x.Id == id && x.Deleted == 0);
        }

        public List<TeamMember> GetAllWithCategory()
        {
            return _context.TeamMembers
                .Include(x => x.TeamCategory)
                .Where(x => x.Deleted == 0)
                .ToList();
        }

        public List<TeamMember> GetActiveMembers()
        {
            return GetAll(x => x.IsActive && x.Deleted == 0);
        }
    }
}
