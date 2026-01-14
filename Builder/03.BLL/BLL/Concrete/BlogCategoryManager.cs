using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Business;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.DTOs.ContentDTO.BlogCategoryDTOs;
using Entities.TableModels.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class BlogCategoryManager : IBlogCategoryService
    {
        private readonly IBlogCategoryDAL _blogCategoryDal;
        private readonly IMapper _mapper;
        public BlogCategoryManager(IBlogCategoryDAL blogCategoryDal, IMapper mapper)
        {
            _blogCategoryDal = blogCategoryDal;
            _mapper = mapper;
        }

        public IResult Add(CreateBlogCategoryDto createBlogCategoryDto)
        {
            var blogCategoryMapper = _mapper.Map<BlogCategory>(createBlogCategoryDto);
            var validateValidator = new BlogCategoryValidation();
            var validationResult = validateValidator.Validate(blogCategoryMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(
                DuplicateBlogCategoryName(blogCategoryMapper),
                DuplicateBlogCategorySlug(blogCategoryMapper)
            );
            if (!checkData.Success)
                return checkData;

            blogCategoryMapper.Deleted = 0;
            _blogCategoryDal.Add(blogCategoryMapper);
            return new SuccessResult("Blog category added successfully");
        }

        public IResult Update(UpdateBlogCategoryDto updateBlogCategoryDto)
        {
            var blogCategoryMapper = _mapper.Map<BlogCategory>(updateBlogCategoryDto);
            var validateValidator = new BlogCategoryValidation();
            var validationResult = validateValidator.Validate(blogCategoryMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(
                DuplicateBlogCategoryName(blogCategoryMapper),
                DuplicateBlogCategorySlug(blogCategoryMapper)
            );
            if (!checkData.Success)
                return checkData;

            _blogCategoryDal.Update(blogCategoryMapper);
            return new SuccessResult("Blog category updated successfully");
        }

        public IResult Delete(int id)
        {
            var blogCategoryGet = _blogCategoryDal.Get(x => x.Id == id && x.Deleted == 0);
            if (blogCategoryGet is not null)
            {
                blogCategoryGet.Deleted = id;
                _blogCategoryDal.Update(blogCategoryGet);
                return new SuccessResult("Blog category deleted successfully");
            }
            return new ErrorResult("Blog category not found");
        }

        public IDataResult<BlogCategoryDto> Get(int id)
        {
            var blogCategory = _blogCategoryDal.Get(x => x.Id == id && x.Deleted == 0);
            if (blogCategory is null)
                return new ErrorDataResult<BlogCategoryDto>("Blog category not found");

            return new SuccessDataResult<BlogCategoryDto>(_mapper.Map<BlogCategoryDto>(blogCategory));
        }

        public IDataResult<List<BlogCategoryDto>> GetAll()
        {
            var blogCategories = _blogCategoryDal.GetAll(x => x.Deleted == 0);
            return new SuccessDataResult<List<BlogCategoryDto>>(_mapper.Map<List<BlogCategoryDto>>(blogCategories));
        }

        public IDataResult<BlogCategoryDetailDto> GetByIdWithPosts(int id)
        {
            using (ApplicationDbContext context = new())
            {
                var blogCategory = context.BlogCategories
                    .Include(x => x.BlogPosts.Where(p => p.Deleted == 0))
                    .FirstOrDefault(x => x.Id == id && x.Deleted == 0);

                if (blogCategory is null)
                    return new ErrorDataResult<BlogCategoryDetailDto>("Blog category not found");

                return new SuccessDataResult<BlogCategoryDetailDto>(_mapper.Map<BlogCategoryDetailDto>(blogCategory));
            }
        }

        public IDataResult<BlogCategoryDetailDto> GetBySlugWithPosts(string slug)
        {
            using (ApplicationDbContext context = new())
            {
                var blogCategory = context.BlogCategories
                    .Include(x => x.BlogPosts.Where(p => p.IsPublished && p.Deleted == 0))
                    .FirstOrDefault(x => x.Slug == slug && x.Deleted == 0);

                if (blogCategory is null)
                    return new ErrorDataResult<BlogCategoryDetailDto>("Blog category not found");

                return new SuccessDataResult<BlogCategoryDetailDto>(_mapper.Map<BlogCategoryDetailDto>(blogCategory));
            }
        }

        public IDataResult<List<BlogCategoryDetailDto>> GetAllWithPosts()
        {
            using (ApplicationDbContext context = new())
            {
                var blogCategories = context.BlogCategories
                    .Include(x => x.BlogPosts.Where(p => p.Deleted == 0))
                    .Where(x => x.Deleted == 0)
                    .ToList();

                return new SuccessDataResult<List<BlogCategoryDetailDto>>(_mapper.Map<List<BlogCategoryDetailDto>>(blogCategories));
            }
        }

        public IDataResult<List<BlogCategoryDetailDto>> GetActiveWithPosts()
        {
            using (ApplicationDbContext context = new())
            {
                var blogCategories = context.BlogCategories
                    .Include(x => x.BlogPosts.Where(p => p.IsPublished && p.Deleted == 0))
                    .Where(x => x.IsActive && x.Deleted == 0)
                    .ToList();

                return new SuccessDataResult<List<BlogCategoryDetailDto>>(_mapper.Map<List<BlogCategoryDetailDto>>(blogCategories));
            }
        }

        private IResult DuplicateBlogCategoryName(BlogCategory blogCategory)
        {
            var categoryName = _blogCategoryDal.Get(x => x.Name == blogCategory.Name && x.Id != blogCategory.Id && x.IsActive && x.Deleted == 0);
            if (categoryName is not null)
            {
                return new ErrorResult("Duplicate blog category name");
            }
            return new SuccessResult();
        }

        private IResult DuplicateBlogCategorySlug(BlogCategory blogCategory)
        {
            var categorySlug = _blogCategoryDal.Get(x => x.Slug == blogCategory.Slug && x.Id != blogCategory.Id && x.Deleted == 0);
            if (categorySlug is not null)
            {
                return new ErrorResult("Duplicate blog category slug");
            }
            return new SuccessResult();
        }
    }

}
