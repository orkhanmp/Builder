using BLL.Abstract;
using Entities.DTOs.ContentDTO.ContactSubmissionDTOs;
using Microsoft.AspNetCore.Mvc;

public class ContactController : Controller
{
    private readonly IContactSubmissionService _contactSubmissionService;
    private readonly ISiteSettingsService _siteSettingsService;
    private readonly IPageBannerService _pageBannerService;

    public ContactController(
        IContactSubmissionService contactSubmissionService,
        ISiteSettingsService siteSettingsService,
        IPageBannerService pageBannerService)
    {
        _contactSubmissionService = contactSubmissionService;
        _siteSettingsService = siteSettingsService;
        _pageBannerService = pageBannerService;
    }

    public IActionResult Index()
    {
        var banner = _pageBannerService.GetByPageName("Contact");
        ViewBag.Banner = banner.Data;

        var settings = _siteSettingsService.GetCurrentSettings();
        ViewBag.Settings = settings.Data;

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(CreateContactSubmissionDto model)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Please fill all required fields correctly.";
            return View(model);
        }

        var result = _contactSubmissionService.Add(model);

        if (result.Success)
        {
            TempData["Success"] = result.Message;
            return RedirectToAction("Index");
        }

        TempData["Error"] = result.Message;
        return View(model);
    }
}