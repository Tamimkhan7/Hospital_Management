using Hospital.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Areas.Admin.Controllers
{
    [Area("admin")]
    public class UserController : Controller
    {
        private IApplicationUserService _userService;

        public UserController(IApplicationUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(int pageNumber = 1,int PageSize=10)
        {
            return View(_userService.GetAll(pageNumber, PageSize));
        }
    }
}
