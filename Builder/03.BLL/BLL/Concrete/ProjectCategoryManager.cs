using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Business;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.DTOs.ContentDTO.ProjectCategoryDTOs;
using Entities.TableModels.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class ProjectCategoryManager : IProjectCategoryService
    {
        private readonly IProjectCategoryDAL _projectCategoryDal;
        private readonly IMapper _mapper;

        public ProjectCategoryManager(IProjectCategoryDAL projectCategoryDal, IMapper mapper)
        {
            _projectCategoryDal = projectCategoryDal;
            _mapper = mapper;
        }

        public IResult Add(CreateProjectCategoryDto createProjectCategoryDto)
        {
            var projectCategoryMapper = _mapper.Map<ProjectCategory>(createProjectCategoryDto);
            var validateValidator = new ProjectCategoryValidation();
            var validationResult = validateValidator.Validate(projectCategoryMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(DuplicateProjectCategoryName(projectCategoryMapper));
            if (!checkData.Success)
                return checkData;

            projectCategoryMapper.Deleted = 0;
            _projectCategoryDal.Add(projectCategoryMapper);
            return new SuccessResult("Project category added successfully");
        }

        public IResult Update(UpdateProjectCategoryDto updateProjectCategoryDto)
        {
            var projectCategoryMapper = _mapper.Map<ProjectCategory>(updateProjectCategoryDto);
            var validateValidator = new ProjectCategoryValidation();
            var validationResult = validateValidator.Validate(projectCategoryMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(DuplicateProjectCategoryName(projectCategoryMapper));
            if (!checkData.Success)
                return checkData;

            _projectCategoryDal.Update(projectCategoryMapper);
            return new SuccessResult("Project category updated successfully");
        }

        public IResult Delete(int id)
        {
            var projectCategoryGet = _projectCategoryDal.Get(x => x.Id == id && x.Deleted == 0);
            if (projectCategoryGet is not null)
            {
                projectCategoryGet.Deleted = id;
                _projectCategoryDal.Update(projectCategoryGet);
                return new SuccessResult("Project category deleted successfully");
            }
            return new ErrorResult("Project category not found");
        }

        public IDataResult<ProjectCategoryDto> Get(int id)
        {
            var projectCategory = _projectCategoryDal.Get(x => x.Id == id && x.Deleted == 0);
            if (projectCategory is null)
                return new ErrorDataResult<ProjectCategoryDto>("Project category not found");

            return new SuccessDataResult<ProjectCategoryDto>(_mapper.Map<ProjectCategoryDto>(projectCategory));
        }

        public IDataResult<List<ProjectCategoryDto>> GetAll()
        {
            var projectCategories = _projectCategoryDal.GetAll(x => x.Deleted == 0);
            return new SuccessDataResult<List<ProjectCategoryDto>>(_mapper.Map<List<ProjectCategoryDto>>(projectCategories));
        }

       public IDataResult<ProjectCategoryDetailDto> GetByIdWithProjects(int id)
        {
            var projectCategory = _projectCategoryDal.GetByIdWithProjects(id);

            if (projectCategory is null)
                return new ErrorDataResult<ProjectCategoryDetailDto>("Project category not found");

            return new SuccessDataResult<ProjectCategoryDetailDto>(_mapper.Map<ProjectCategoryDetailDto>(projectCategory));
        }

        public IDataResult<List<ProjectCategoryDetailDto>> GetAllWithProjects()
        {
            var projectCategories = _projectCategoryDal.GetAllWithProjects();
            return new SuccessDataResult<List<ProjectCategoryDetailDto>>(_mapper.Map<List<ProjectCategoryDetailDto>>(projectCategories));
        }

        public IDataResult<List<ProjectCategoryDetailDto>> GetActiveWithProjects()
        {
            var projectCategories = _projectCategoryDal.GetActiveWithProjects();
            return new SuccessDataResult<List<ProjectCategoryDetailDto>>(_mapper.Map<List<ProjectCategoryDetailDto>>(projectCategories));
        }

        private IResult DuplicateProjectCategoryName(ProjectCategory projectCategory)
        {
            var categoryName = _projectCategoryDal.Get(x => x.Name == projectCategory.Name && x.Id != projectCategory.Id && x.Deleted == 0);
            if (categoryName is not null)
            {
                return new ErrorResult("Duplicate project category name");
            }
            return new SuccessResult();
        }
    }
}
