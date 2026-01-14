using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Business;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.DTOs.ContentDTO.TeamCategoryDTOs;
using Entities.TableModels.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class TeamCategoryManager : ITeamCategoryService
    {
        private readonly ITeamCategoryDAL _teamCategoryDal;
        private readonly IMapper _mapper;

        public TeamCategoryManager(ITeamCategoryDAL teamCategoryDal, IMapper mapper)
        {
            _teamCategoryDal = teamCategoryDal;
            _mapper = mapper;
        }

        public IResult Add(CreateTeamCategoryDto createTeamCategoryDto)
        {
            var teamCategoryMapper = _mapper.Map<TeamCategory>(createTeamCategoryDto);
            var validateValidator = new TeamCategoryValidation();
            var validationResult = validateValidator.Validate(teamCategoryMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(DuplicateTeamCategoryName(teamCategoryMapper));
            if (!checkData.Success)
                return checkData;

            teamCategoryMapper.Deleted = 0;
            _teamCategoryDal.Add(teamCategoryMapper);
            return new SuccessResult("Team category added successfully");
        }

        public IResult Update(UpdateTeamCategoryDto updateTeamCategoryDto)
        {
            var teamCategoryMapper = _mapper.Map<TeamCategory>(updateTeamCategoryDto);
            var validateValidator = new TeamCategoryValidation();
            var validationResult = validateValidator.Validate(teamCategoryMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(DuplicateTeamCategoryName(teamCategoryMapper));
            if (!checkData.Success)
                return checkData;

            _teamCategoryDal.Update(teamCategoryMapper);
            return new SuccessResult("Team category updated successfully");
        }

        public IResult Delete(int id)
        {
            var teamCategoryGet = _teamCategoryDal.Get(x => x.Id == id && x.Deleted == 0);
            if (teamCategoryGet is not null)
            {
                teamCategoryGet.Deleted = id;
                _teamCategoryDal.Update(teamCategoryGet);
                return new SuccessResult("Team category deleted successfully");
            }
            return new ErrorResult("Team category not found");
        }

        public IDataResult<TeamCategoryDto> Get(int id)
        {
            var teamCategory = _teamCategoryDal.Get(x => x.Id == id && x.Deleted == 0);
            if (teamCategory is null)
                return new ErrorDataResult<TeamCategoryDto>("Team category not found");

            return new SuccessDataResult<TeamCategoryDto>(_mapper.Map<TeamCategoryDto>(teamCategory));
        }

        public IDataResult<List<TeamCategoryDto>> GetAll()
        {
            var teamCategories = _teamCategoryDal.GetAll(x => x.Deleted == 0);
            return new SuccessDataResult<List<TeamCategoryDto>>(_mapper.Map<List<TeamCategoryDto>>(teamCategories));
        }

        public IDataResult<TeamCategoryDetailDto> GetByIdWithMembers(int id)
        {
            using (ApplicationDbContext context = new())
            {
                var teamCategory = context.TeamCategories
                    .Include(x => x.TeamMembers.Where(m => m.Deleted == 0))
                    .FirstOrDefault(x => x.Id == id && x.Deleted == 0);

                if (teamCategory is null)
                    return new ErrorDataResult<TeamCategoryDetailDto>("Team category not found");

                return new SuccessDataResult<TeamCategoryDetailDto>(_mapper.Map<TeamCategoryDetailDto>(teamCategory));
            }
        }

        public IDataResult<List<TeamCategoryDetailDto>> GetAllWithMembers()
        {
            using (ApplicationDbContext context = new())
            {
                var teamCategories = context.TeamCategories
                    .Include(x => x.TeamMembers.Where(m => m.Deleted == 0))
                    .Where(x => x.Deleted == 0)
                    .ToList();

                return new SuccessDataResult<List<TeamCategoryDetailDto>>(_mapper.Map<List<TeamCategoryDetailDto>>(teamCategories));
            }
        }

        public IDataResult<List<TeamCategoryDetailDto>> GetActiveWithMembers()
        {
            using (ApplicationDbContext context = new())
            {
                var teamCategories = context.TeamCategories
                    .Include(x => x.TeamMembers.Where(m => m.IsActive && m.Deleted == 0))
                    .Where(x => x.IsActive && x.Deleted == 0)
                    .ToList();

                return new SuccessDataResult<List<TeamCategoryDetailDto>>(_mapper.Map<List<TeamCategoryDetailDto>>(teamCategories));
            }
        }

        private IResult DuplicateTeamCategoryName(TeamCategory teamCategory)
        {
            var categoryName = _teamCategoryDal.Get(x => x.Name == teamCategory.Name && x.Id != teamCategory.Id && x.IsActive && x.Deleted == 0);
            if (categoryName is not null)
            {
                return new ErrorResult("Duplicate team category name");
            }
            return new SuccessResult();
        }
    }
}
