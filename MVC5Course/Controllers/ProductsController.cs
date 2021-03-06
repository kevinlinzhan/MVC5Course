﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;
using System.Data.Entity.Validation;
using PagedList;
using PagedList.Mvc;

namespace MVC5Course.Controllers
{
    [HandleError(ExceptionType = typeof(DbEntityValidationException), 
        View = "Error_DbEntityValidationException")]
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        
        ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
        public ActionResult Index(int pageNo = 1)
        {
            //var repo = new ProductRepository();
            //repo.UnitOfWork = new EFUnitOfWork();

            var data = repo.All().OrderBy(p => p.ProductId).AsQueryable();
            
            return View(data.ToPagedList(pageNo, 10));
            //return View(repo.GetTop(10));
        }

        // GET: Products/Details/5
        //[Route("pro/del/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                var db = repo.UnitOfWork.Context;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Product product = repo.Find(id);
            repo.Delete(product);        
            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ProductList()
        {
            var data = db.Product.OrderByDescending(p => p.ProductId).Take(5);
            return View(data);
        }

        public ActionResult BatchUpdate(ProductBatchUpdateViewModel[] items)
        {
            //if (ModelState.IsValid)
            //{
            //    foreach (var item in items)
            //    {
            //        var product = db.Product.Find(item.ProductId);
            //        product.ProductName = item.ProductName;
            //        product.Active = item.Active;
            //        product.Stock = item.Stock;
            //        product.Price = item.Price;
            //    }

            //    try
            //    {

            //        db.SaveChanges();
            //    }
            //    catch (DbEntityValidationException ex)
            //    {
            //        foreach (var entityErrors in ex.EntityValidationErrors)
            //        {
            //            foreach (var vErrors in entityErrors.ValidationErrors)
            //            {
            //                throw new DbEntityValidationException(vErrors.PropertyName + " 發生錯誤：" + vErrors.ErrorMessage);
            //            }
            //        }
            //    }

            //    return RedirectToAction("ProductList");
            //}



            //if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var product = db.Product.Find(item.ProductId);
                    product.ProductName = item.ProductName;
                    product.Active = item.Active;
                    product.Stock = item.Stock;
                    product.Price = item.Price;
                }

                db.SaveChanges();

                return RedirectToAction("ProductList");
            }

            return View();
        }
    }
}
