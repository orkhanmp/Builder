using BLL.Abstract;
using Entities.DTOs.ContentDTO.TeamMamberDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Builder.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMemberController : Controller
    {
        private readonly ITeamMemberService _teamMemberService;
        private readonly ITeamCategoryService _teamCategoryService;

        public TeamMemberController(ITeamMemberService teamMemberService, ITeamCategoryService teamCategoryService)
        {
            _teamMemberService = teamMemberService;
            _teamCategoryService = teamCategoryService;
        }

        public IActionResult Index()
        {
            var result = _teamMemberService.GetAllWithCategory();
            return View(result.Data);
        }

        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateTeamMemberDto model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View(model);
            }

            var result = _teamMemberService.Add(model);

            if (result.Success)
            {
                TempData["Success"] = result.Message;
                return RedirectToAction("Index");
            }

            TempData["Error"] = result.Message;
            LoadCategories();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var result = _teamMemberService.Get(id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }

            var updateDto = new UpdateTeamMemberDto
            {
                Id = result.Data.Id,
                FullName = result.Data.FullName,
                Position = result.Data.Position,
                ImageUrl = result.Data.ImageUrl,               
                TeamCategoryId = result.Data.TeamCategoryId,
                DisplayOrder = result.Data.DisplayOrder,
                IsActive = result.Data.IsActive
            };

            LoadCategories();
            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateTeamMemberDto model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View(model);
            }

            var result = _teamMemberService.Update(model);

            if (result.Success)
            {
                TempData["Success"] = result.Message;
                return RedirectToAction("Index");
            }

            TempData["Error"] = result.Message;
            LoadCategories();
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _teamMemberService.Delete(id);

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
            var categories = _teamCategoryService.GetAll();
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
        }
    }
}