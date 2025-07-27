using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Areas.Admin.Controllers
{
    [Area("admin")]
    public class UserController : Controller
    {
        private readonly IApplicationUserService _userService;

        public UserController(IApplicationUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var model = _userService.GetAll(pageNumber, pageSize);
            return View(model);
        }

        public IActionResult AllDoctors(int pageNumber = 1, int pageSize = 10)
        {
            var model = _userService.GetAllDoctors(pageNumber, pageSize);
            return View(model);
        }

        public IActionResult AllPatient(int pageNumber = 1, int pageSize = 10)
        {
            var model = _userService.GetAllPatient(pageNumber, pageSize);
            return View(model);
        }

        public IActionResult DoctorFind(int pageNumber = 1, int pageSize = 10, string Specility = null)
        {
            var model = _userService.SearchDoctor(pageNumber, pageSize, Specility);
            return View(model);
        }

        // CREATE GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                _userService.CreateUser(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // EDIT GET
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                _userService.UpdateUser(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // DELETE GET (Confirm delete page optional)
        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
