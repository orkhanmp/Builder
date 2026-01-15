using BLL.Abstract;
using Entities.DTOs.ContentDTO.ProjectDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Builder.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IProjectCategoryService _projectCategoryService;

        public ProjectController(IProjectService projectService, IProjectCategoryService projectCategoryService)
        {
            _projectService = projectService;
            _projectCategoryService = projectCategoryService;
        }

        public IActionResult Index()
        {
            var result = _projectService.GetAllWithCategory();
            return View(result.Data);
        }

        public IActionResult Create()
        {
            LoadCategories();
            LoadStatuses();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProjectDto model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                LoadStatuses();
                return View(model);
            }

            var result = _projectService.Add(model);

            if (result.Success)
            {
                TempData["Success"] = result.Message;
                return RedirectToAction("Index");
            }

            TempData["Error"] = result.Message;
            LoadCategories();
            LoadStatuses();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var result = _projectService.Get(id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }

            var updateDto = new UpdateProjectDto
            {
                Id = result.Data.Id,
                Title = result.Data.Title,
                ShortDescription = result.Data.ShortDescription,
                Location = result.Data.Location,
                Status = result.Data.Status,
                MainImageUrl = result.Data.MainImageUrl,
                ProjectCategoryId = result.Data.ProjectCategoryId,
                IsFeatured = result.Data.IsFeatured,
                IsActive = result.Data.IsActive
            };

            LoadCategories();
            LoadStatuses();
            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateProjectDto model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                LoadStatuses();
                return View(model);
            }

            var result = _projectService.Update(model);

            if (result.Success)
            {
                TempData["Success"] = result.Message;
                return RedirectToAction("Index");
            }

            TempData["Error"] = result.Message;
            LoadCategories();
            LoadStatuses();
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _projectService.Delete(id);

            if (result.Success)
            {
                TempData["Success"] = result.Message;
            }
            else
            {
                TempData["Error"] = result.Message;
            }

            return RedirectToAction("Index");
        }

        private void LoadCategories()
        {
            var categories = _projectCategoryService.GetAll();
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
        }

        private void LoadStatuses()
        {
            ViewBag.Statuses = new SelectList(new[] { "Complete", "Running", "Upcoming" });
        }
    }
}