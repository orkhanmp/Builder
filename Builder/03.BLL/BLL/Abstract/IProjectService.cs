using Core.Result.Abstract;
using Entities.DTOs.ContentDTO.ProjectDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IProjectService
    {
        IResult Add(CreateProjectDto createProjectDto);
        IResult Update(UpdateProjectDto updateProjectDto);
        IResult Delete(int id);
        IDataResult<List<ProjectDto>> GetAll();
        IDataResult<ProjectDto> Get(int id);
        IDataResult<List<ProjectDto>> GetByCategoryId(int categoryId);
        IDataResult<List<ProjectDto>> GetByStatus(string status);
        IDataResult<List<ProjectDto>> GetFeaturedProjects();
        IDataResult<ProjectDetailDto> GetByIdWithCategory(int id);
        IDataResult<List<ProjectDto>> GetAllWithCategory();
    }
}
