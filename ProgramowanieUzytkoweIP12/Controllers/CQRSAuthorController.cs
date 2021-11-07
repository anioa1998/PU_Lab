using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramowanieUzytkoweIP12.Controllers
{
    public class CQRSAuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
