using BLL.Abstract;
using Microsoft.AspNetCore.Mvc;

public class TeamController : Controller
{
    private readonly ITeamMemberService _teamMemberService;
    private readonly ITeamCategoryService _teamCategoryService;
    private readonly IPageBannerService _pageBannerService;

    public TeamController(
        ITeamMemberService teamMemberService,
        ITeamCategoryService teamCategoryService,
        IPageBannerService pageBannerService)
    {
        _teamMemberService = teamMemberService;
        _teamCategoryService = teamCategoryService;
        _pageBannerService = pageBannerService;
    }

    public IActionResult Index(int? categoryId)
    {
        var banner = _pageBannerService.GetByPageName("Team");
        ViewBag.Banner = banner.Data;

        var categories = _teamCategoryService.GetAll();
        ViewBag.Categories = categories.Data;

        var teamMembers = categoryId.HasValue
            ? _teamMemberService.GetByCategoryId(categoryId.Value)
            : _teamMemberService.GetAllWithCategory();

        ViewBag.SelectedCategoryId = categoryId;

        return View(teamMembers.Data);
    }

    public IActionResult Detail(int id)
    {
        var teamMember = _teamMemberService.GetByIdWithCategory(id);
        if (!teamMember.Success)
        {
            TempData["Error"] = teamMember.Message;
            return RedirectToAction("Index");
        }

        return View(teamMember.Data);
    }
}