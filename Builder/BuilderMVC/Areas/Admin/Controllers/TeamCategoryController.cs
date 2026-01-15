using BLL.Abstract;
using Entities.DTOs.ContentDTO.TeamCategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Builder.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamCategoryController : Controller
    {
        private readonly ITeamCategoryService _teamCategoryService;

        public TeamCategoryController(ITeamCategoryService teamCategoryService)
        {
            _teamCategoryService = teamCategoryService;
        }

        public IActionResult Index()
        {
            var result = _teamCategoryService.GetAll();
            return View(result.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateTeamCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _teamCategoryService.Add(model);

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
            var result = _teamCategoryService.Get(id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }

            var updateDto = new UpdateTeamCategoryDto
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
        public IActionResult Edit(UpdateTeamCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _teamCategoryService.Update(model);

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
            var result = _teamCategoryService.Delete(id);

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