using MVC5Course.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MVController : Controller
    {
        // GET: MV
        [ShareData]
        [LocalDebugOnly]
        public ActionResult Index()
        {
            ViewData["Temp1"] = "暫存資料";

            var b = new ClientLoginViewModel()
            {
                FirstName="lin",
                LastName="kevin"
            };

            ViewData["Temp2"] = b;

            ViewBag.Temp3 = b;

            return View();
        }

        [HttpPost]
        public ActionResult MyForm(ClientLoginViewModel c)
        {
            if(ModelState.IsValid)
            {
                TempData["MyFormData"] = c;
                return RedirectToAction("MyFormResult");
            }

            return View();
        }

        public ActionResult MyFormResult()
        {
            return View();
        }
    }
}