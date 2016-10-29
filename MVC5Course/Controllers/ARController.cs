using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : BaseController
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        // GET: AR
        public ActionResult Test()
        {
            return View();
        }

        public PartialViewResult partialViewTest()
        {
            return PartialView();
        }

        public ContentResult contentResultTest()
        {
            return Content("<root><text>123</text></root>", "text/xml", System.Text.Encoding.UTF8);
        }

        public ActionResult FileTest()
        {
            var filePath = Server.MapPath("~/Content/下載.jpg");
            
            return File(filePath, "image/jpeg");
        }

        public ActionResult FileTest2()
        {
            var filePath = Server.MapPath("~/Content/下載.jpg");

            return File(filePath, "image/jpeg", "test.jpg");
        }

        public ActionResult JsonTest()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var data = db.Product.OrderBy(p => p.ProductId).Take(10);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}