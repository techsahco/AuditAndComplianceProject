using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using adminlte.Models;

namespace adminlte.Controllers
{
    public class CategoriesController : Controller
    {
        private AuditCompliance db = new AuditCompliance();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var isExist = db.Categories.Where(x => x.CategoryName == category.CategoryName).FirstOrDefault();
                if (isExist == null)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, errorMesage = "Same Name already exist" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return Json(new { success = true, data = category }, JsonRequestBehavior.AllowGet);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                Category cat = db.Categories.Find(category.CategoryID);
                if (cat == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    var isExist = db.Categories.Where(x => x.CategoryName == category.CategoryName && x.CategoryID != category.CategoryID).FirstOrDefault();
                    if (isExist == null)
                    {
                        cat.CategoryName = category.CategoryName;
                        db.SaveChanges();
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { success = false, errorMesage = "Same Name already exist" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Category cat = db.Categories.Find(id);
                if (cat != null)
                {
                    db.Categories.Remove(cat);
                    db.SaveChanges();
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            catch
            {

                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
