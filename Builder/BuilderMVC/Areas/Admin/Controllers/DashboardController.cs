using BLL.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Builder.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly ITeamMemberService _teamMemberService;
        private readonly IProjectService _projectService;
        private readonly IBlogPostService _blogPostService;
        private readonly IContactSubmissionService _contactSubmissionService;

        public DashboardController(
            IServiceService serviceService,
            ITeamMemberService teamMemberService,
            IProjectService projectService,
            IBlogPostService blogPostService,
            IContactSubmissionService contactSubmissionService)
        {
            _serviceService = serviceService;
            _teamMemberService = teamMemberService;
            _projectService = projectService;
            _blogPostService = blogPostService;
            _contactSubmissionService = contactSubmissionService;
        }

        public IActionResult Index()
        {
            var services = _serviceService.GetAll();
            ViewBag.ServiceCount = services.Data?.Count ?? 0;

            var teamMembers = _teamMemberService.GetAll();
            ViewBag.TeamMemberCount = teamMembers.Data?.Count ?? 0;

            var projects = _projectService.GetAll();
            ViewBag.ProjectCount = projects.Data?.Count ?? 0;

            var blogPosts = _blogPostService.GetAll();
            ViewBag.BlogPostCount = blogPosts.Data?.Count ?? 0;

            var contactMessages = _contactSubmissionService.GetUnread();
            ViewBag.UnreadMessages = contactMessages.Data?.Count ?? 0;

            return View();
        }
    }
}