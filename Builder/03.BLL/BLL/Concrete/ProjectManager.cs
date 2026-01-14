using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Business;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.DTOs.ContentDTO.ProjectDTOs;
using Entities.TableModels.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class ProjectManager : IProjectService
    {
        private readonly IProjectDAL _projectDal;
        private readonly IMapper _mapper;

        public ProjectManager(IProjectDAL projectDal, IMapper mapper)
        {
            _projectDal = projectDal;
            _mapper = mapper;
        }

        public IResult Add(CreateProjectDto createProjectDto)
        {
            var projectMapper = _mapper.Map<Project>(createProjectDto);
            var validateValidator = new ProjectValidation();
            var validationResult = validateValidator.Validate(projectMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(DuplicateProjectTitle(projectMapper));
            if (!checkData.Success)
                return checkData;

            projectMapper.Deleted = 0;
            _projectDal.Add(projectMapper);
            return new SuccessResult("Project added successfully");
        }

        public IResult Update(UpdateProjectDto updateProjectDto)
        {
            var projectMapper = _mapper.Map<Project>(updateProjectDto);
            var validateValidator = new ProjectValidation();
            var validationResult = validateValidator.Validate(projectMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(DuplicateProjectTitle(projectMapper));
            if (!checkData.Success)
                return checkData;

            _projectDal.Update(projectMapper);
            return new SuccessResult("Project updated successfully");
        }

        public IResult Delete(int id)
        {
            var projectGet = _projectDal.Get(x => x.Id == id && x.Deleted == 0);
            if (projectGet is not null)
            {
                projectGet.Deleted = id;
                _projectDal.Update(projectGet);
                return new SuccessResult("Project deleted successfully");
            }
            return new ErrorResult("Project not found");
        }

        public IDataResult<ProjectDto> Get(int id)
        {
            var project = _projectDal.Get(x => x.Id == id && x.Deleted == 0);
            if (project is null)
                return new ErrorDataResult<ProjectDto>("Project not found");

            return new SuccessDataResult<ProjectDto>(_mapper.Map<ProjectDto>(project));
        }

        public IDataResult<List<ProjectDto>> GetAll()
        {
            var projects = _projectDal.GetAll(x => x.Deleted == 0);
            return new SuccessDataResult<List<ProjectDto>>(_mapper.Map<List<ProjectDto>>(projects));
        }

        public IDataResult<List<ProjectDto>> GetByCategoryId(int categoryId)
        {
            var projects = _projectDal.GetAll(x => x.ProjectCategoryId == categoryId && x.Deleted == 0);
            return new SuccessDataResult<List<ProjectDto>>(_mapper.Map<List<ProjectDto>>(projects));
        }

        public IDataResult<List<ProjectDto>> GetByStatus(string status)
        {
            var projects = _projectDal.GetAll(x => x.Status == status && x.Deleted == 0);
            return new SuccessDataResult<List<ProjectDto>>(_mapper.Map<List<ProjectDto>>(projects));
        }

        public IDataResult<List<ProjectDto>> GetFeaturedProjects()
        {
            var projects = _projectDal.GetAll(x => x.IsFeatured && x.IsActive && x.Deleted == 0);
            return new SuccessDataResult<List<ProjectDto>>(_mapper.Map<List<ProjectDto>>(projects));
        }

        public IDataResult<ProjectDetailDto> GetByIdWithCategory(int id)
        {
            using (ApplicationDbContext context = new())
            {
                var project = context.Projects
                    .Include(x => x.ProjectCategory)
                    .FirstOrDefault(x => x.Id == id && x.Deleted == 0);

                if (project is null)
                    return new ErrorDataResult<ProjectDetailDto>("Project not found");

                return new SuccessDataResult<ProjectDetailDto>(_mapper.Map<ProjectDetailDto>(project));
            }
        }

        public IDataResult<List<ProjectDto>> GetAllWithCategory()
        {
            using (ApplicationDbContext context = new())
            {
                var projects = context.Projects
                    .Include(x => x.ProjectCategory)
                    .Where(x => x.Deleted == 0)
                    .ToList();

                return new SuccessDataResult<List<ProjectDto>>(_mapper.Map<List<ProjectDto>>(projects));
            }
        }

        private IResult DuplicateProjectTitle(Project project)
        {
            var projectTitle = _projectDal.Get(x => x.Title == project.Title && x.Id != project.Id && x.IsActive && x.Deleted == 0);
            if (projectTitle is not null)
            {
                return new ErrorResult("Duplicate project title");
            }
            return new SuccessResult();
        }
    }
}
