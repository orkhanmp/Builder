using Core.Result.Abstract;
using Entities.DTOs.ContentDTO.BlogPostDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IBlogPostService
    {
        IResult Add(CreateBlogPostDto createBlogPostDto);
        IResult Update(UpdateBlogPostDto updateBlogPostDto);
        IResult Delete(int id);
        IDataResult<List<BlogPostDto>> GetAll();
        IDataResult<BlogPostDto> Get(int id);
        IDataResult<List<BlogPostDto>> GetByCategoryId(int categoryId);
        IDataResult<BlogPostDetailDto> GetBySlug(string slug);
        IDataResult<BlogPostDetailDto> GetByIdWithCategory(int id);
        IDataResult<List<BlogPostDto>> GetAllWithCategory();
        IDataResult<List<BlogPostDto>> GetPublishedPosts();
        IDataResult<List<BlogPostDto>> GetRecentPosts(int count);
        IResult IncrementViewCount(int id);
        IDataResult<List<BlogPostDto>> Search(string searchTerm);
    }
}
