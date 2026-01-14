using BLL.Abstract;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IHomeSliderService _homeSliderService;
    private readonly IAboutService _aboutService;
    private readonly IServiceService _serviceService;
    private readonly ITeamMemberService _teamMemberService;
    private readonly IProjectService _projectService;
    private readonly IBlogPostService _blogPostService;

    public HomeController(
        IHomeSliderService homeSliderService,
        IAboutService aboutService,
        IServiceService serviceService,
        ITeamMemberService teamMemberService,
        IProjectService projectService,
        IBlogPostService blogPostService)
    {
        _homeSliderService = homeSliderService;
        _aboutService = aboutService;
        _serviceService = serviceService;
        _teamMemberService = teamMemberService;
        _projectService = projectService;
        _blogPostService = blogPostService;
    }

    public IActionResult Index()
    {
        var sliders = _homeSliderService.GetByDisplayOrder();
        ViewBag.Sliders = sliders.Data;

        var about = _aboutService.GetCurrent();
        ViewBag.About = about.Data;

        var services = _serviceService.GetActiveServices();
        ViewBag.Services = services.Data?.Take(6).ToList();

        var teamMembers = _teamMemberService.GetActiveMembers();
        ViewBag.TeamMembers = teamMembers.Data?.Take(4).ToList();

        var projects = _projectService.GetFeaturedProjects();
        ViewBag.Projects = projects.Data?.Take(6).ToList();

        var blogPosts = _blogPostService.GetRecentPosts(3);
        ViewBag.BlogPosts = blogPosts.Data;

        return View();
    }
}