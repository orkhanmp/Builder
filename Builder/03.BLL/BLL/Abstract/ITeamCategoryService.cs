using Core.Result.Abstract;
using Entities.DTOs.ContentDTO.TeamCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ITeamCategoryService
    {
        IResult Add(CreateTeamCategoryDto createTeamCategoryDto);
        IResult Update(UpdateTeamCategoryDto updateTeamCategoryDto);
        IResult Delete(int id);
        IDataResult<List<TeamCategoryDto>> GetAll();
        IDataResult<TeamCategoryDto> Get(int id);
        IDataResult<TeamCategoryDetailDto> GetByIdWithMembers(int id);
        IDataResult<List<TeamCategoryDetailDto>> GetAllWithMembers();
        IDataResult<List<TeamCategoryDetailDto>> GetActiveWithMembers();
    }
}
