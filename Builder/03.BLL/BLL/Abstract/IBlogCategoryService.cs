using Core.Result.Abstract;
using Entities.DTOs.ContentDTO.BlogCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IBlogCategoryService
    {
        IResult Add(CreateBlogCategoryDto createBlogCategoryDto);
        IResult Update(UpdateBlogCategoryDto updateBlogCategoryDto);
        IResult Delete(int id);
        IDataResult<List<BlogCategoryDto>> GetAll();
        IDataResult<BlogCategoryDto> Get(int id);
        IDataResult<BlogCategoryDetailDto> GetByIdWithPosts(int id);
        IDataResult<BlogCategoryDetailDto> GetBySlugWithPosts(string slug);
        IDataResult<List<BlogCategoryDetailDto>> GetAllWithPosts();
        IDataResult<List<BlogCategoryDetailDto>> GetActiveWithPosts();
    }
}
