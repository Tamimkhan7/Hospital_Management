using Hospital.Model;
using Hospital.Services;
using Hospital.Utilities;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HospitalManagement.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    //[Authorize(Roles = WebSiteRoles.WebSite_Doctor)]
    public class DoctorsController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_doctorService.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult AddTiming()
        {
            List<SelectListItem> morningShiftStart = new();
            List<SelectListItem> morningShiftEnd = new();
            List<SelectListItem> afternoonShiftStart = new();
            List<SelectListItem> afternoonShiftEnd = new();

            for (int i = 1; i <= 11; i++)
                morningShiftStart.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            for (int i = 1; i <= 13; i++)
                morningShiftEnd.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            for (int i = 13; i <= 16; i++)
                afternoonShiftStart.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            for (int i = 13; i <= 18; i++)
                afternoonShiftEnd.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            ViewBag.morningStart = new SelectList(morningShiftStart, "Value", "Text");
            ViewBag.morningEnd = new SelectList(morningShiftEnd, "Value", "Text");
            ViewBag.evenStart = new SelectList(afternoonShiftStart, "Value", "Text");
            ViewBag.evenEnd = new SelectList(afternoonShiftEnd, "Value", "Text");

            var vm = new TimingViewModel
            {
                SheduleDate = DateTime.Now.AddDays(1)
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddTiming(TimingViewModel timing)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                timing.DoctorId = Guid.Parse(claim.Value); 
                _doctorService.AddTiming(timing);
                return RedirectToAction("Index");
            }

            return View(timing);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _doctorService.GetTimingById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(TimingViewModel vm)
        {
            _doctorService.UpdateTiming(vm);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _doctorService.DeleteTiming(id);
            return RedirectToAction("Index");
        }
    }
}
