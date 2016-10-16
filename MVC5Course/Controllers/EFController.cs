using MVC5Course.Models;
using MVC5Course.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();

        // GET: EF
        public ActionResult Index()
        {
            var data = db.Product.Where(p => p.ProductName.Contains("White") || p.ProductName.Contains("Word")).
                            OrderByDescending(p => p.ProductId).Take(10);

            return View(data);
        }

        public ActionResult Create()
        {
            Product product = new Product()
            {
                ProductName = "kevinWord",
                Price = 100,
                Active = true,
                Stock = 10
            };

            db.Product.Add(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);
            
            db.OrderLine.RemoveRange(product.OrderLine);

            db.Product.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var product = db.Product.Find(id);
            
            return View(product);
        }

        public ActionResult Update(int id)
        {
            var product = db.Product.Find(id);
            product.ProductName = product.ProductName + "!";
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        //public ActionResult Add20Percent()
        //{
        //    var data = db.Product.Where(p => p.ProductName.Contains("White") || p.ProductName.Contains("Word"));

        //    foreach(var item in data)
        //    {
        //        if(item.Price.HasValue)
        //        {
        //            item.Price = item.Price * 1.2m;
        //        }
        //    }

        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        public ActionResult Add20Percent()
        {
            string str = "%White%";
            db.Database.ExecuteSqlCommand("Update dbo.Product Set Price=Price*1.2 Where ProductName Like @p0", str);

            return RedirectToAction("Index");
        }

        public ActionResult ClientContribution()
        {
            var data = db.vw_ClientContribution.Where(p => p.ProductName.Contains("White") || p.ProductName.Contains("Word")).
                            OrderByDescending(p => p.ProductId).Take(10);

            return View(data);
        }

        public ActionResult ClientContribution2(string kw)
        {
            kw = "%"+ kw + "%";
            var data = db.Database.SqlQuery<ClientContributionViewModel>(
                "SELECT ProductName, Price, Active,Stock,IsDelete,               "+
                "(                                                                          "+
                " select sum(qty) from dbo.OrderLine where ProductId = a.ProductId          "+
                "                                                                           "+
                ") as OrderTotal                                                            "+
                "                                                                           "+
                "FROM dbo.Product a                                                        "+
                "Where ProductName Like @p0",
                kw);

            return View(data);
        }
    }
}