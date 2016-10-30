using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class OrdersController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: Orders
        public ActionResult Index(int ClientId)
        {
            var order = db.Order.Include(o => o.Client).Where(c => c.ClientId == ClientId);
            return View(order.ToList());
        }

        
    }
}
