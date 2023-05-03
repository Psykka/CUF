using Microsoft.AspNetCore.Mvc;

namespace CUF.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public IActionResult Login()
        {
            // handle user login action
            // create session
            // redirect to SupplierController List
            return RedirectToAction("List", "Supplier");
        }

        [HttpPost]
        public IActionResult Register()
        {
            // handle user register
            // save to new user to database
            // create session
            // redirect to SupplierController List
            return RedirectToAction("List", "Supplier");
        }
    }
}
