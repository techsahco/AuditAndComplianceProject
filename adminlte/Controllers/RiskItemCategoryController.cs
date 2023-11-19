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
    public class RiskItemCategoryController : Controller
    {
        private readonly AuditCompliance _context = new AuditCompliance();

        // GET: AuditClass
        public ActionResult Index()
        {
            return View(new GenericAuditViewModel<RiskItemCategory>
            {
                AuditModel = _context.RiskItemCategories
                .Include(a => a.AuditClass)
                .Include(a => a.AuditClass.AuditCategory)
                .ToList(),
                ParentAuditDataModel = _context.AuditClasses.Select(x => new AuditModel { Code = x.AuditClassCode, Name = x.AuditClassName }).ToList()
            });
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(AuditModel auditModel)
        {
            var isExist = _context.RiskItemCategories.Where(x => x.RiskItemCategoryCode == auditModel.Code).FirstOrDefault();

            if (isExist != null) return Json(new { success = false, errorMesage = "Same Code already exist" }, JsonRequestBehavior.AllowGet);

            _context.RiskItemCategories.Add(GetChildAuditModel(auditModel));
            _context.SaveChanges();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: auditChildModel/Edit/5
        public ActionResult Edit(string code)
        {
            if (code == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var auditChildModel = _context.RiskItemCategories.Find(code);

            if (auditChildModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return Json(new 
            { 
                success = true, 
                data = new AuditModel 
                { 
                    Code       = auditChildModel.RiskItemCategoryCode, 
                    Name       = auditChildModel.RiskItemCategoryName, 
                    ParentCode = auditChildModel.AuditClassCode 
                } 
            }, JsonRequestBehavior.AllowGet);
        }

        // POST: auditChildModel/Edit/5
        [HttpPost]
        public ActionResult Edit(AuditModel auditModel)
        {
            if (auditModel.Code == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var auditChildModel = _context.RiskItemCategories.Find(auditModel.Code);

            if (auditChildModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            auditChildModel.RiskItemCategoryName = auditModel.Name;
            auditChildModel.AuditClassCode = auditModel.ParentCode;
            _context.Entry(auditChildModel).State = EntityState.Modified;
            _context.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // POST: AuditCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string code)
        {
            try
            {
                var auditChildModel = _context.RiskItemCategories.Find(code);
                if (auditChildModel == null) return Json(new { success = false }, JsonRequestBehavior.AllowGet);

                _context.RiskItemCategories.Remove(auditChildModel);
                _context.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
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

        private RiskItemCategory GetChildAuditModel(AuditModel auditModel)
        {
            return new RiskItemCategory
            {
                AuditClassCode = auditModel.ParentCode,
                RiskItemCategoryName = auditModel.Name,
                RiskItemCategoryCode = auditModel.Code
            };
        }
    }
}
