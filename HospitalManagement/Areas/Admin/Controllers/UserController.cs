using Hospital.Services;
using Hospital.Utilities;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HospitalManagement.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IApplicationUserService _userService;

        public UserController(IApplicationUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = WebSiteRoles.WebSite_Admin)]
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


        [HttpGet]
        [Authorize(Roles = WebSiteRoles.WebSite_Admin)]
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WebSiteRoles.WebSite_Admin)]
        public IActionResult Create(ApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                _userService.CreateUser(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
   
        [HttpGet]
        [Authorize(Roles = $"{WebSiteRoles.WebSite_Admin},{WebSiteRoles.WebSite_Doctor},{WebSiteRoles.WebSite_Patient}")]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();

            if (User.IsInRole(WebSiteRoles.WebSite_Doctor) || User.IsInRole(WebSiteRoles.WebSite_Patient))
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (currentUserId != id) return Forbid();
            }

            return View(user);
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{WebSiteRoles.WebSite_Admin},{WebSiteRoles.WebSite_Doctor},{WebSiteRoles.WebSite_Patient}")]
        public IActionResult Edit(ApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole(WebSiteRoles.WebSite_Doctor) || User.IsInRole(WebSiteRoles.WebSite_Patient))
                {
                    var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (currentUserId != model.Id)
                        return Forbid();
                }

                _userService.UpdateUser(model); 
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = $"{WebSiteRoles.WebSite_Admin},{WebSiteRoles.WebSite_Doctor},{WebSiteRoles.WebSite_Patient}")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();

            if (User.IsInRole(WebSiteRoles.WebSite_Doctor) || User.IsInRole(WebSiteRoles.WebSite_Patient))
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (currentUserId != id) return Forbid();
            }

            return View(user);
        }
    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{WebSiteRoles.WebSite_Admin},{WebSiteRoles.WebSite_Doctor},{WebSiteRoles.WebSite_Patient}")]
        public IActionResult DeleteConfirmed(string id)
        {
            if (User.IsInRole(WebSiteRoles.WebSite_Doctor) || User.IsInRole(WebSiteRoles.WebSite_Patient))
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (currentUserId != id) return Forbid();
            }

            _userService.DeleteUser(id);

            if (!User.IsInRole(WebSiteRoles.WebSite_Admin))
                return RedirectToAction("Logout", "Account", new { area = "" });

            return RedirectToAction(nameof(Index));
        }
    }
}
