using BLL.Abstract;
using Microsoft.AspNetCore.Mvc;

public class TeamController : Controller
{
    private readonly ITeamMemberService _teamMemberService;
    private readonly IPageBannerService _pageBannerService;

    public TeamController(ITeamMemberService teamMemberService, IPageBannerService pageBannerService)
    {
        _teamMemberService = teamMemberService;
        _pageBannerService = pageBannerService;
    }

    public IActionResult Index()
    {
        var banner = _pageBannerService.GetByPageName("Team");
        ViewBag.Banner = banner.Data;

        var teamMembers = _teamMemberService.GetAllWithCategory();
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