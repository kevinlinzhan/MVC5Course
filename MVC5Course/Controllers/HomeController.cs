using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            //Session["a"] = "sessionTest";
            //Thread.Sleep(10000);
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page." + Session["a"];
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult GetTime()
         {
             return Content(DateTime.Now.ToString());
         }

    public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel Login, string ReturnUrl)
        {
            if(ModelState.IsValid)
            {
                if(Login.Email=="td1628@hotmail.com" && Login.Password=="123456")
                {
                    FormsAuthentication.RedirectFromLoginPage(Login.Email, false);
                    Redirect(ReturnUrl ?? "/");
                }
            }

            return View();
        }
    }
}