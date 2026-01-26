using Blok34.Domain.Identity;
using Blok34.Service.Implementation;
using Blok34.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blok34.Web.Controllers
{
    public class ProfileController : Controller
    {
       // private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {       
                _userService = userService;   
        }

        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = _userService.GetUserById(id);

            if (user == null)
                return NotFound();

            return View(user);
        }
    }
}
