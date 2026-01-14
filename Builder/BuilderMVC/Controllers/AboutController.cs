using BLL.Abstract;
using Microsoft.AspNetCore.Mvc;

public class AboutController : Controller
{
    private readonly IAboutService _aboutService;
    private readonly IPageBannerService _pageBannerService;

    public AboutController(IAboutService aboutService, IPageBannerService pageBannerService)
    {
        _aboutService = aboutService;
        _pageBannerService = pageBannerService;
    }

    public IActionResult Index()
    {
        var banner = _pageBannerService.GetByPageName("About");
        ViewBag.Banner = banner.Data;

        var about = _aboutService.GetCurrent();
        if (!about.Success)
        {
            TempData["Error"] = about.Message;
            return RedirectToAction("Index", "Home");
        }

        return View(about.Data);
    }
}