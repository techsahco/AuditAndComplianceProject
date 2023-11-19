using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using adminlte.Models;
using adminlte.ViewModels;
using Microsoft.AspNet.Identity;

namespace adminlte.Controllers
{
    public class AuditCategoryController : Controller
    {
        private readonly AuditCompliance _context = new AuditCompliance();

        // GET: AuditCategory
        public ActionResult Index()
        {
            var auditCategories = _context.AuditCategories.ToList();
            return View(auditCategories);
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(AuditModel auditModel)
        {
            var isExist = _context.AuditCategories.Where(x => x.AuditCategoryCode == auditModel.Code).FirstOrDefault();

            if (isExist != null) return Json(new { success = false, errorMesage = "Same Code already exist" }, JsonRequestBehavior.AllowGet);

            _context.AuditCategories.Add(GetChildAuditModel(auditModel));
            _context.SaveChanges();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: AuditCategory/Edit/5
        public ActionResult Edit(string code)
        {
            if (code == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           
            var auditCategory = _context.AuditCategories.Find(code);

            if (auditCategory == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return Json(new { success = true, data = new AuditModel { Code = auditCategory.AuditCategoryCode, Name = auditCategory.AuditCategoryName } }, JsonRequestBehavior.AllowGet);
        }

        // POST: AuditCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(AuditModel auditModel)
        {
            if (auditModel.Code == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var auditCategory = _context.AuditCategories.Find(auditModel.Code);

            if (auditCategory == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            auditCategory.AuditCategoryName = auditModel.Name;
            //_context.Entry(GetChildAuditModel(auditModel)).State = EntityState.Modified;
            _context.Entry(auditCategory).State = EntityState.Modified;
            _context.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // POST: AuditCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string code)
        {
            try
            {
                var auditCategory = _context.AuditCategories.Find(code);
                if (auditCategory == null) return Json(new { success = false }, JsonRequestBehavior.AllowGet);
               
                _context.AuditCategories.Remove(auditCategory);
                _context.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, errorMesage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private AuditCategory GetChildAuditModel(AuditModel auditModel)
        {
            return new AuditCategory
            {
                AuditCategoryCode = auditModel.Code,
                AuditCategoryName = auditModel.Name,
            };
        }
    }

}
