using BLL.Abstract;
using Entities.DTOs.ContentDTO.HomeSliderDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Builder.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeSliderController : Controller
    {
        private readonly IHomeSliderService _homeSliderService;

        public HomeSliderController(IHomeSliderService homeSliderService)
        {
            _homeSliderService = homeSliderService;
        }

        public IActionResult Index()
        {
            var result = _homeSliderService.GetAll();
            return View(result.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateHomeSliderDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _homeSliderService.Add(model);

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
            var result = _homeSliderService.Get(id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }

            var updateDto = new UpdateHomeSliderDto
            {
                Id = result.Data.Id,
                SubTitle = result.Data.SubTitle,
                MainHeading = result.Data.MainHeading,
                ButtonText = result.Data.ButtonText,
                ButtonUrl = result.Data.ButtonUrl,
                BackgroundImageUrl = result.Data.BackgroundImageUrl,
                DisplayOrder = result.Data.DisplayOrder,
                IsActive = result.Data.IsActive
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateHomeSliderDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _homeSliderService.Update(model);

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
            var result = _homeSliderService.Delete(id);

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