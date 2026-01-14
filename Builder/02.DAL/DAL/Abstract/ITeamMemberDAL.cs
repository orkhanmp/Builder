using Core.Abstract;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface ITeamMemberDAL : IBaseRepository<TeamMember>
    {
        List<TeamMember> GetByCategoryId(int categoryId);
        TeamMember GetByIdWithCategory(int id);
        List<TeamMember> GetAllWithCategory();
        List<TeamMember> GetActiveMembers();
    }
}
