using BLL.Abstract;
using Entities.DTOs.ContentDTO.ProjectCategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Builder.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectCategoryController : Controller
    {
        private readonly IProjectCategoryService _projectCategoryService;

        public ProjectCategoryController(IProjectCategoryService projectCategoryService)
        {
            _projectCategoryService = projectCategoryService;
        }

        public IActionResult Index()
        {
            var result = _projectCategoryService.GetAll();
            return View(result.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProjectCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _projectCategoryService.Add(model);

            if (result.Success)
            {
                TempData["Success"] = result.Message;
                return RedirectToAction("Index");
            }

            TempData["Error"] = result.Message;
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var result = _projectCategoryService.Get(id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }

            var updateDto = new UpdateProjectCategoryDto
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                Description = result.Data.Description,
                DisplayOrder = result.Data.DisplayOrder,
                IsActive = result.Data.IsActive
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateProjectCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _projectCategoryService.Update(model);

            if (result.Success)
            {
                TempData["Success"] = result.Message;
                return RedirectToAction("Index");
            }

            TempData["Error"] = result.Message;
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _projectCategoryService.Delete(id);

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
    }
}