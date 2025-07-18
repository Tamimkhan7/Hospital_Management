using Hospital.Model;
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalManagement.Areas.Admin.Controllers
{

    [Area("admin")]
    public class ContactsController : Controller
    {

        private IContactService _Contact; 

        public ContactsController(IContactService Contact)
        {
            _Contact = Contact;

        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_Contact.GetAll(pageNumber, pageSize));
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _Contact.GetContactById(id);
            return View(viewModel);
        }

        [HttpPost]

        public IActionResult Edit(ContactViewModel vm)
        {
            _Contact.UpdateContact(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]

        public IActionResult Create(ContactViewModel vm)
        {
            _Contact.InsertContact(vm);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _Contact.DeleteContact(id);
            return RedirectToAction("Index");

        }
    }
}
