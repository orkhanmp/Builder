using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.DTOs.ContentDTO.TeamMamberDTOs;
using Entities.TableModels.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class TeamMemberManager : ITeamMemberService
    {
        private readonly ITeamMemberDAL _teamMemberDal;
        private readonly IMapper _mapper;

        public TeamMemberManager(ITeamMemberDAL teamMemberDal, IMapper mapper)
        {
            _teamMemberDal = teamMemberDal;
            _mapper = mapper;
        }

        public IResult Add(CreateTeamMemberDto createTeamMemberDto)
        {
            var teamMemberMapper = _mapper.Map<TeamMember>(createTeamMemberDto);
            var validateValidator = new TeamMemberValidation();
            var validationResult = validateValidator.Validate(teamMemberMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            teamMemberMapper.Deleted = 0;
            _teamMemberDal.Add(teamMemberMapper);
            return new SuccessResult("Team member added successfully");
        }

        public IResult Update(UpdateTeamMemberDto updateTeamMemberDto)
        {
            var teamMemberMapper = _mapper.Map<TeamMember>(updateTeamMemberDto);
            var validateValidator = new TeamMemberValidation();
            var validationResult = validateValidator.Validate(teamMemberMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            _teamMemberDal.Update(teamMemberMapper);
            return new SuccessResult("Team member updated successfully");
        }

        public IResult Delete(int id)
        {
            var teamMemberGet = _teamMemberDal.Get(x => x.Id == id && x.Deleted == 0);
            if (teamMemberGet is not null)
            {
                teamMemberGet.Deleted = id;
                _teamMemberDal.Update(teamMemberGet);
                return new SuccessResult("Team member deleted successfully");
            }
            return new ErrorResult("Team member not found");
        }

        public IDataResult<TeamMemberDto> Get(int id)
        {
            var teamMember = _teamMemberDal.Get(x => x.Id == id && x.Deleted == 0);
            if (teamMember is null)
                return new ErrorDataResult<TeamMemberDto>("Team member not found");

            return new SuccessDataResult<TeamMemberDto>(_mapper.Map<TeamMemberDto>(teamMember));
        }

        public IDataResult<List<TeamMemberDto>> GetAll()
        {
            var teamMembers = _teamMemberDal.GetAll(x => x.Deleted == 0);
            return new SuccessDataResult<List<TeamMemberDto>>(_mapper.Map<List<TeamMemberDto>>(teamMembers));
        }

        public IDataResult<List<TeamMemberDto>> GetByCategoryId(int categoryId)
        {
            var teamMembers = _teamMemberDal.GetAll(x => x.TeamCategoryId == categoryId && x.Deleted == 0);
            return new SuccessDataResult<List<TeamMemberDto>>(_mapper.Map<List<TeamMemberDto>>(teamMembers));
        }

        public IDataResult<TeamMemberDetailDto> GetByIdWithCategory(int id)
        {
            using (ApplicationDbContext context = new())
            {
                var teamMember = context.TeamMembers
                    .Include(x => x.TeamCategory)
                    .FirstOrDefault(x => x.Id == id && x.Deleted == 0);

                if (teamMember is null)
                    return new ErrorDataResult<TeamMemberDetailDto>("Team member not found");

                return new SuccessDataResult<TeamMemberDetailDto>(_mapper.Map<TeamMemberDetailDto>(teamMember));
            }
        }

        public IDataResult<List<TeamMemberDto>> GetAllWithCategory()
        {
            using (ApplicationDbContext context = new())
            {
                var teamMembers = context.TeamMembers
                    .Include(x => x.TeamCategory)
                    .Where(x => x.Deleted == 0)
                    .ToList();

                return new SuccessDataResult<List<TeamMemberDto>>(_mapper.Map<List<TeamMemberDto>>(teamMembers));
            }
        }

        public IDataResult<List<TeamMemberDto>> GetActiveMembers()
        {
            var teamMembers = _teamMemberDal.GetAll(x => x.IsActive && x.Deleted == 0);
            return new SuccessDataResult<List<TeamMemberDto>>(_mapper.Map<List<TeamMemberDto>>(teamMembers));
        }
    }
}
