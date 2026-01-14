using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Business;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.DTOs.ContentDTO.BlogPostDTOs;
using Entities.TableModels.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class BlogPostManager : IBlogPostService
    {
        private readonly IBlogPostDAL _blogPostDal;
        private readonly IMapper _mapper;
        public BlogPostManager(IBlogPostDAL blogPostDal, IMapper mapper)
        {
            _blogPostDal = blogPostDal;
            _mapper = mapper;
        }

        public IResult Add(CreateBlogPostDto createBlogPostDto)
        {
            var blogPostMapper = _mapper.Map<BlogPost>(createBlogPostDto);
            var validateValidator = new BlogPostValidation();
            var validationResult = validateValidator.Validate(blogPostMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(
                DuplicateBlogPostTitle(blogPostMapper),
                DuplicateBlogPostSlug(blogPostMapper)
            );
            if (!checkData.Success)
                return checkData;

            blogPostMapper.Deleted = 0;
            _blogPostDal.Add(blogPostMapper);
            return new SuccessResult("Blog post added successfully");
        }

        public IResult Update(UpdateBlogPostDto updateBlogPostDto)
        {
            var blogPostMapper = _mapper.Map<BlogPost>(updateBlogPostDto);
            var validateValidator = new BlogPostValidation();
            var validationResult = validateValidator.Validate(blogPostMapper);
            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            var checkData = BusinessRules.Check(
                DuplicateBlogPostTitle(blogPostMapper),
                DuplicateBlogPostSlug(blogPostMapper)
            );
            if (!checkData.Success)
                return checkData;

            _blogPostDal.Update(blogPostMapper);
            return new SuccessResult("Blog post updated successfully");
        }

        public IResult Delete(int id)
        {
            var blogPostGet = _blogPostDal.Get(x => x.Id == id && x.Deleted == 0);
            if (blogPostGet is not null)
            {
                blogPostGet.Deleted = id;
                _blogPostDal.Update(blogPostGet);
                return new SuccessResult("Blog post deleted successfully");
            }
            return new ErrorResult("Blog post not found");
        }

        public IDataResult<BlogPostDto> Get(int id)
        {
            var blogPost = _blogPostDal.Get(x => x.Id == id && x.Deleted == 0);
            if (blogPost is null)
                return new ErrorDataResult<BlogPostDto>("Blog post not found");

            return new SuccessDataResult<BlogPostDto>(_mapper.Map<BlogPostDto>(blogPost));
        }

        public IDataResult<List<BlogPostDto>> GetAll()
        {
            var blogPosts = _blogPostDal.GetAll(x => x.Deleted == 0);
            return new SuccessDataResult<List<BlogPostDto>>(_mapper.Map<List<BlogPostDto>>(blogPosts));
        }

        public IDataResult<List<BlogPostDto>> GetByCategoryId(int categoryId)
        {
            var blogPosts = _blogPostDal.GetAll(x => x.BlogCategoryId == categoryId && x.Deleted == 0);
            return new SuccessDataResult<List<BlogPostDto>>(_mapper.Map<List<BlogPostDto>>(blogPosts));
        }

        public IDataResult<BlogPostDetailDto> GetBySlug(string slug)
        {
            using (ApplicationDbContext context = new())
            {
                var blogPost = context.BlogPosts
                    .Include(x => x.BlogCategory)
                    .FirstOrDefault(x => x.Slug == slug && x.Deleted == 0);

                if (blogPost is null)
                    return new ErrorDataResult<BlogPostDetailDto>("Blog post not found");

                return new SuccessDataResult<BlogPostDetailDto>(_mapper.Map<BlogPostDetailDto>(blogPost));
            }
        }

        public IDataResult<BlogPostDetailDto> GetByIdWithCategory(int id)
        {
            using (ApplicationDbContext context = new())
            {
                var blogPost = context.BlogPosts
                    .Include(x => x.BlogCategory)
                    .FirstOrDefault(x => x.Id == id && x.Deleted == 0);

                if (blogPost is null)
                    return new ErrorDataResult<BlogPostDetailDto>("Blog post not found");

                return new SuccessDataResult<BlogPostDetailDto>(_mapper.Map<BlogPostDetailDto>(blogPost));
            }
        }

        public IDataResult<List<BlogPostDto>> GetAllWithCategory()
        {
            using (ApplicationDbContext context = new())
            {
                var blogPosts = context.BlogPosts
                    .Include(x => x.BlogCategory)
                    .Where(x => x.Deleted == 0)
                    .OrderByDescending(x => x.PublishDate)
                    .ToList();

                return new SuccessDataResult<List<BlogPostDto>>(_mapper.Map<List<BlogPostDto>>(blogPosts));
            }
        }

        public IDataResult<List<BlogPostDto>> GetPublishedPosts()
        {
            var blogPosts = _blogPostDal.GetAll(x => x.IsPublished && x.PublishDate <= DateTime.Now && x.Deleted == 0);
            return new SuccessDataResult<List<BlogPostDto>>(_mapper.Map<List<BlogPostDto>>(blogPosts));
        }

        public IDataResult<List<BlogPostDto>> GetRecentPosts(int count)
        {
            using (ApplicationDbContext context = new())
            {
                var blogPosts = context.BlogPosts
                    .Where(x => x.IsPublished && x.Deleted == 0)
                    .OrderByDescending(x => x.PublishDate)
                    .Take(count)
                    .ToList();

                return new SuccessDataResult<List<BlogPostDto>>(_mapper.Map<List<BlogPostDto>>(blogPosts));
            }
        }

        public IResult IncrementViewCount(int id)
        {
            using (ApplicationDbContext context = new())
            {
                var post = context.BlogPosts.FirstOrDefault(x => x.Id == id && x.Deleted == 0);
                if (post is null)
                    return new ErrorResult("Blog post not found");

                post.ViewCount++;
                context.SaveChanges();
                return new SuccessResult("View count incremented");
            }
        }

        public IDataResult<List<BlogPostDto>> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new ErrorDataResult<List<BlogPostDto>>("Search term cannot be empty");

            using (ApplicationDbContext context = new())
            {
                var blogPosts = context.BlogPosts
                    .Where(x => x.IsPublished && x.Deleted == 0 &&
                        (x.Title.Contains(searchTerm) || x.Summary.Contains(searchTerm)))
                    .OrderByDescending(x => x.PublishDate)
                    .ToList();

                return new SuccessDataResult<List<BlogPostDto>>(_mapper.Map<List<BlogPostDto>>(blogPosts));
            }
        }

        private IResult DuplicateBlogPostTitle(BlogPost blogPost)
        {
            var postTitle = _blogPostDal.Get(x => x.Title == blogPost.Title && x.Id != blogPost.Id && x.Deleted == 0);
            if (postTitle is not null)
            {
                return new ErrorResult("Duplicate blog post title");
            }
            return new SuccessResult();
        }

        private IResult DuplicateBlogPostSlug(BlogPost blogPost)
        {
            var postSlug = _blogPostDal.Get(x => x.Slug == blogPost.Slug && x.Id != blogPost.Id && x.Deleted == 0);
            if (postSlug is not null)
            {
                return new ErrorResult("Duplicate blog post slug");
            }
            return new SuccessResult();
        }
    }
}
