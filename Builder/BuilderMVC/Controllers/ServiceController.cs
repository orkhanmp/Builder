using BLL.Abstract;
using Microsoft.AspNetCore.Mvc;

public class ServiceController : Controller
{
    private readonly IServiceService _serviceService;
    private readonly IServiceCategoryService _serviceCategoryService;
    private readonly IPageBannerService _pageBannerService;

    public ServiceController(
        IServiceService serviceService,
        IServiceCategoryService serviceCategoryService,
        IPageBannerService pageBannerService)
    {
        _serviceService = serviceService;
        _serviceCategoryService = serviceCategoryService;
        _pageBannerService = pageBannerService;
    }

    public IActionResult Index(int? categoryId)
    {
        var banner = _pageBannerService.GetByPageName("Service");
        ViewBag.Banner = banner.Data;

        var categories = _serviceCategoryService.GetAll();
        ViewBag.Categories = categories.Data;

        var services = categoryId.HasValue
            ? _serviceService.GetByCategoryId(categoryId.Value)
            : _serviceService.GetAllWithCategory();

        ViewBag.SelectedCategoryId = categoryId;

        return View(services.Data);
    }

    public IActionResult Detail(int id)
    {
        var service = _serviceService.GetByIdWithCategory(id);
        if (!service.Success)
        {
            TempData["Error"] = service.Message;
            return RedirectToAction("Index");
        }

        var relatedServices = _serviceService.GetByCategoryId(service.Data.ServiceCategoryId ?? 0);
        ViewBag.RelatedServices = relatedServices.Data?.Where(x => x.Id != id).Take(3).ToList();

        return View(service.Data);
    }
}