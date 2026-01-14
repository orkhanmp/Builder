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
        public List<TeamMember> GetByCategoryId(int categoryId)
        {
            return GetAll(x => x.TeamCategoryId == categoryId);
        }

        public TeamMember GetByIdWithCategory(int id)
        {
            using (ApplicationDbContext context = new())
            {
                return context.TeamMembers
                    .Include(x => x.TeamCategory)
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public List<TeamMember> GetAllWithCategory()
        {
            using (ApplicationDbContext context = new())
            {
                return context.TeamMembers
                    .Include(x => x.TeamCategory)
                    .ToList();
            }
        }

        public List<TeamMember> GetActiveMembers()
        {
            return GetAll(x => x.IsActive);
        }
    }
}
