using BLL.Abstract;
using Microsoft.AspNetCore.Mvc;

public class ProjectController : Controller
{
    private readonly IProjectService _projectService;
    private readonly IProjectCategoryService _projectCategoryService;
    private readonly IPageBannerService _pageBannerService;

    public ProjectController(
        IProjectService projectService,
        IProjectCategoryService projectCategoryService,
        IPageBannerService pageBannerService)
    {
        _projectService = projectService;
        _projectCategoryService = projectCategoryService;
        _pageBannerService = pageBannerService;
    }

    public IActionResult Index(string status = "All")
    {
        var banner = _pageBannerService.GetByPageName("Project");
        ViewBag.Banner = banner.Data;

        var projects = status == "All"
            ? _projectService.GetAllWithCategory()
            : _projectService.GetByStatus(status);

        ViewBag.CurrentStatus = status;
        return View(projects.Data);
    }

    public IActionResult Detail(int id)
    {
        var project = _projectService.GetByIdWithCategory(id);
        if (!project.Success)
        {
            TempData["Error"] = project.Message;
            return RedirectToAction("Index");
        }

        var relatedProjects = _projectService.GetByCategoryId(project.Data.ProjectCategoryId);
        ViewBag.RelatedProjects = relatedProjects.Data?.Where(x => x.Id != id).Take(3).ToList();

        return View(project.Data);
    }
}