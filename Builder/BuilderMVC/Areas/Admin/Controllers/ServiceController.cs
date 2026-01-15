using BLL.Abstract;
using Entities.DTOs.ContentDTO.ServiceDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Builder.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IServiceCategoryService _serviceCategoryService;

        public ServiceController(IServiceService serviceService, IServiceCategoryService serviceCategoryService)
        {
            _serviceService = serviceService;
            _serviceCategoryService = serviceCategoryService;
        }

        public IActionResult Index()
        {
            var result = _serviceService.GetAllWithCategory();
            return View(result.Data);
        }

        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateServiceDto model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View(model);
            }

            var result = _serviceService.Add(model);

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
            var result = _serviceService.Get(id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }

            var updateDto = new UpdateServiceDto
            {
                Id = result.Data.Id,
                Title = result.Data.Title,
                ShortDescription = result.Data.ShortDescription,
                DetailedDescription = result.Data.ShortDescription,
                ImageUrl = result.Data.ImageUrl,
                IconClass = result.Data.IconClass,
                ServiceCategoryId = result.Data.ServiceCategoryId,
                DisplayOrder = result.Data.DisplayOrder,
                IsActive = result.Data.IsActive
            };

            LoadCategories();
            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateServiceDto model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View(model);
            }

            var result = _serviceService.Update(model);

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
            var result = _serviceService.Delete(id);

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
            var categories = _serviceCategoryService.GetAll();
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
        }
    }
}