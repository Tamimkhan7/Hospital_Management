using Hospital.Model;
using Hospital.Services;
using Hospital.Utilities;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Areas.Admin.Controllers
{

    [Area("admin")]    
    [Authorize]
    public class RoomsController : Controller
    {

        private IRoomService _Room;

        public RoomsController(IRoomService Room)
        {
            _Room = Room;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_Room.GetAll(pageNumber, pageSize));
        }


        [HttpGet]
        [Authorize(Roles = WebSiteRoles.WebSite_Admin)]
        public IActionResult Edit(int id)
        {
            var viewModel = _Room.GetRoomById(id);
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = WebSiteRoles.WebSite_Admin)]

        public IActionResult Edit(RoomViewModel vm)
        {
            _Room.UpdateRoom(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = WebSiteRoles.WebSite_Admin)]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = WebSiteRoles.WebSite_Admin)]

        public IActionResult Create(RoomViewModel vm)
        {
            _Room.InsertRoom(vm);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = WebSiteRoles.WebSite_Admin)]
        public IActionResult Delete(int id)
        {
            _Room.DeleteRoom(id);
            return RedirectToAction("Index");

        }
    }
}
