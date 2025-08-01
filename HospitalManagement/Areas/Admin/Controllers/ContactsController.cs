using Hospital.Services;
using Hospital.Utilities;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalManagement.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IContactService _Contact;

        public ContactsController(IContactService contact)
        {
            _Contact = contact;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_Contact.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        [Authorize(Roles = WebSiteRoles.WebSite_Admin)]
        public IActionResult Create()
        {
            ViewBag.Hospitals = new SelectList(_Contact.GetHospitalInfos(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = WebSiteRoles.WebSite_Admin)]
        public IActionResult Create(ContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _Contact.InsertContact(vm);
                return RedirectToAction("Index");
            }
            ViewBag.Hospitals = new SelectList(_Contact.GetHospitalInfos(), "Id", "Name", vm.HospitalInfoId);
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = WebSiteRoles.WebSite_Admin)]
        public IActionResult Edit(int id)
        {
            var vm = _Contact.GetContactById(id);
            ViewBag.Hospitals = new SelectList(_Contact.GetHospitalInfos(), "Id", "Name", vm.HospitalInfoId);
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = WebSiteRoles.WebSite_Admin)]
        public IActionResult Edit(ContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _Contact.UpdateContact(vm);
                return RedirectToAction("Index");
            }
            ViewBag.Hospitals = new SelectList(_Contact.GetHospitalInfos(), "Id", "Name", vm.HospitalInfoId);
            return View(vm);
        }

        [Authorize(Roles = WebSiteRoles.WebSite_Admin)]
        public IActionResult Delete(int id)
        {
            _Contact.DeleteContact(id);
            return RedirectToAction("Index");
        }
    }
}
