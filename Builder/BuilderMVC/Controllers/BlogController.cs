using BLL.Abstract;
using Core.Result.Abstract;
using Entities.DTOs.ContentDTO.BlogPostDTOs;
using Microsoft.AspNetCore.Mvc;

public class BlogController : Controller
{
    private readonly IBlogPostService _blogPostService;
    private readonly IBlogCategoryService _blogCategoryService;
    private readonly IPageBannerService _pageBannerService;

    public BlogController(
        IBlogPostService blogPostService,
        IBlogCategoryService blogCategoryService,
        IPageBannerService pageBannerService)
    {
        _blogPostService = blogPostService;
        _blogCategoryService = blogCategoryService;
        _pageBannerService = pageBannerService;
    }

    public IActionResult Index(int? categoryId, string search)
    {
        var banner = _pageBannerService.GetByPageName("Blog");
        ViewBag.Banner = banner.Data;

        var categories = _blogCategoryService.GetAll();
        ViewBag.Categories = categories.Data;

        IDataResult<List<BlogPostDto>> posts;

        if (!string.IsNullOrEmpty(search))
        {
            posts = _blogPostService.Search(search);
            ViewBag.SearchTerm = search;
        }
        else if (categoryId.HasValue)
        {
            posts = _blogPostService.GetByCategoryId(categoryId.Value);
            ViewBag.SelectedCategoryId = categoryId;
        }
        else
        {
            posts = _blogPostService.GetPublishedPosts();
        }

        return View(posts.Data);
    }

    public IActionResult Detail(string slug)
    {
        var post = _blogPostService.GetBySlug(slug);
        if (!post.Success)
        {
            TempData["Error"] = post.Message;
            return RedirectToAction("Index");
        }

        _blogPostService.IncrementViewCount(post.Data.Id);

        var recentPosts = _blogPostService.GetRecentPosts(5);
        ViewBag.RecentPosts = recentPosts.Data?.Where(x => x.Id != post.Data.Id).Take(4).ToList();

        var categories = _blogCategoryService.GetAll();
        ViewBag.Categories = categories.Data;

        return View(post.Data);
    }
}