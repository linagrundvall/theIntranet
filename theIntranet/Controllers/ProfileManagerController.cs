using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using theIntranet.Models;

namespace theIntranet.Controllers
{
    public class ProfileManagerController : Controller
    {

        public ProfileManagerController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}