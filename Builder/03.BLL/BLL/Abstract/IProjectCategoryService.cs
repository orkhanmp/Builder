using Core.Result.Abstract;
using Entities.DTOs.ContentDTO.ProjectCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IProjectCategoryService
    {
        IResult Add(CreateProjectCategoryDto createProjectCategoryDto);
        IResult Update(UpdateProjectCategoryDto updateProjectCategoryDto);
        IResult Delete(int id);
        IDataResult<List<ProjectCategoryDto>> GetAll();
        IDataResult<ProjectCategoryDto> Get(int id);
        IDataResult<ProjectCategoryDetailDto> GetByIdWithProjects(int id);
        IDataResult<List<ProjectCategoryDetailDto>> GetAllWithProjects();
        IDataResult<List<ProjectCategoryDetailDto>> GetActiveWithProjects();
    }
}
