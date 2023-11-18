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
    public class RiskItemCetogoryController : Controller
    {
        private readonly AuditCompliance _context = new AuditCompliance();

        // GET: AuditClass
        public ActionResult Index()
        {
            var auditClasses = _context.AuditClasses.Include(a => a.AuditCategory).ToList();
            return View(auditClasses);
        }

        // GET: AuditClass/Create
        public ActionResult Create()
        {
            ViewBag.AuditCategoryCode = new SelectList(_context.AuditCategories, "AuditCategoryCode", "AuditCategoryName");
            return View();
        }

        // POST: AuditClass/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuditClass auditClass)
        {
            if (ModelState.IsValid)
            {
                _context.AuditClasses.Add(auditClass);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuditCategoryCode = new SelectList(_context.AuditCategories, "AuditCategoryCode", "AuditCategoryName", auditClass.AuditCategoryCode);
            return View(auditClass);
        }

        // GET: AuditClass/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var auditClass = _context.AuditClasses.Find(id);

            if (auditClass == null)
            {
                return HttpNotFound();
            }

            ViewBag.AuditCategoryCode = new SelectList(_context.AuditCategories, "AuditCategoryCode", "AuditCategoryName", auditClass.AuditCategoryCode);
            return View(auditClass);
        }

        // POST: AuditClass/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuditClass auditClass)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(auditClass).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuditCategoryCode = new SelectList(_context.AuditCategories, "AuditCategoryCode", "AuditCategoryName", auditClass.AuditCategoryCode);
            return View(auditClass);
        }

        // GET: AuditClass/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var auditClass = _context.AuditClasses.Find(id);

            if (auditClass == null)
            {
                return HttpNotFound();
            }

            return View(auditClass);
        }

        // POST: AuditClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var auditClass = _context.AuditClasses.Find(id);
            _context.AuditClasses.Remove(auditClass);
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
