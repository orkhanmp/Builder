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
        public TeamCategory GetByIdWithMembers(int id)
        {
            using (ApplicationDbContext context = new())
            {
                return context.TeamCategories
                    .Include(x => x.TeamMembers)
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public List<TeamCategory> GetAllWithMembers()
        {
            using (ApplicationDbContext context = new())
            {
                return context.TeamCategories
                    .Include(x => x.TeamMembers)
                    .ToList();
            }
        }

        public List<TeamCategory> GetActiveWithMembers()
        {
            using (ApplicationDbContext context = new())
            {
                return context.TeamCategories
                    .Include(x => x.TeamMembers.Where(m => m.IsActive))
                    .Where(x => x.IsActive)
                    .ToList();
            }
        }
    }
}
