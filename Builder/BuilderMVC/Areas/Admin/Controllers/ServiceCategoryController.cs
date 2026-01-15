using BLL.Abstract;
using Entities.DTOs.ContentDTO.ServiceCategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Builder.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceCategoryController : Controller
    {
        private readonly IServiceCategoryService _serviceCategoryService;

        public ServiceCategoryController(IServiceCategoryService serviceCategoryService)
        {
            _serviceCategoryService = serviceCategoryService;
        }

        public IActionResult Index()
        {
            var result = _serviceCategoryService.GetAll();
            return View(result.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateServiceCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _serviceCategoryService.Add(model);

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
            var result = _serviceCategoryService.Get(id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }

            var updateDto = new UpdateServiceCategoryDto
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                Description = result.Data.Description,
                IconClass = result.Data.IconClass,
                DisplayOrder = result.Data.DisplayOrder,
                IsActive = result.Data.IsActive
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateServiceCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _serviceCategoryService.Update(model);

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
            var result = _serviceCategoryService.Delete(id);

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