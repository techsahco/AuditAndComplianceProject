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
    public class RiskItemSubClassController : Controller
    {
        private readonly AuditCompliance _context = new AuditCompliance();

        // GET: AuditCategory
        public ActionResult Index()
        {
            var auditCategories = _context.AuditCategories.ToList();
            return View(auditCategories);
        }

        // GET: AuditCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuditCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuditCategory auditCategory)
        {
            if (ModelState.IsValid)
            {
                _context.AuditCategories.Add(auditCategory);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(auditCategory);
        }

        // GET: AuditCategory/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var auditCategory = _context.AuditCategories.Find(id);

            if (auditCategory == null)
            {
                return HttpNotFound();
            }

            return View(auditCategory);
        }

        // POST: AuditCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuditCategory auditCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(auditCategory).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(auditCategory);
        }

        // GET: AuditCategory/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var auditCategory = _context.AuditCategories.Find(id);

            if (auditCategory == null)
            {
                return HttpNotFound();
            }

            return View(auditCategory);
        }

        // POST: AuditCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var auditCategory = _context.AuditCategories.Find(id);
            _context.AuditCategories.Remove(auditCategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}
