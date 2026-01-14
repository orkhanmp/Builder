using Core.Result.Abstract;
using Entities.DTOs.ContentDTO.TeamMamberDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ITeamMemberService
    {
        IResult Add(CreateTeamMemberDto createTeamMemberDto);
        IResult Update(UpdateTeamMemberDto updateTeamMemberDto);
        IResult Delete(int id);
        IDataResult<List<TeamMemberDto>> GetAll();
        IDataResult<TeamMemberDto> Get(int id);
        IDataResult<List<TeamMemberDto>> GetByCategoryId(int categoryId);
        IDataResult<TeamMemberDetailDto> GetByIdWithCategory(int id);
        IDataResult<List<TeamMemberDto>> GetAllWithCategory();
        IDataResult<List<TeamMemberDto>> GetActiveMembers();
    }
}
