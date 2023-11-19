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

        // GET: AuditClass
        public ActionResult Index()
        {
            return View(new GenericAuditViewModel<RiskItemSubClass>
            {
                AuditModel = _context.RiskItemSubClasses
                .Include(a => a.RiskItemClass)
                .Include(a => a.RiskItemClass.RiskItemCategory)
                .Include(a => a.RiskItemClass.RiskItemCategory.AuditClass)
                .Include(a => a.RiskItemClass.RiskItemCategory.AuditClass.AuditCategory)
                .ToList(),
                ParentAuditDataModel = _context.RiskItemClasses.Select(x => new AuditModel { Code = x.RiskItemClassCode, Name = x.RiskItemClassName }).ToList()
            });
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(AuditModel auditModel)
        {
            var isExist = _context.RiskItemSubClasses.Where(x => x.RiskItemSubClassCode == auditModel.Code).FirstOrDefault();

            if (isExist != null) return Json(new { success = false, errorMesage = "Same Code already exist" }, JsonRequestBehavior.AllowGet);

            _context.RiskItemSubClasses.Add(GetChildAuditModel(auditModel));
            _context.SaveChanges();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: auditChildModel/Edit/5
        public ActionResult Edit(string code)
        {
            if (code == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var auditChildModel = _context.RiskItemSubClasses.Find(code);

            if (auditChildModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return Json(new
            {
                success = true,
                data = new AuditModel
                {
                    Code = auditChildModel.RiskItemSubClassCode,
                    Name = auditChildModel.RiskItemSubClassName,
                    ParentCode = auditChildModel.RiskItemClassCode
                }
            }, JsonRequestBehavior.AllowGet);
        }

        // POST: auditChildModel/Edit/5
        [HttpPost]
        public ActionResult Edit(AuditModel auditModel)
        {
            if (auditModel.Code == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var auditChildModel = _context.RiskItemSubClasses.Find(auditModel.Code);

            if (auditChildModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            auditChildModel.RiskItemSubClassName = auditModel.Name;
            auditChildModel.RiskItemClassCode = auditModel.ParentCode;
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
                var auditChildModel = _context.RiskItemSubClasses.Find(code);
                if (auditChildModel == null) return Json(new { success = false }, JsonRequestBehavior.AllowGet);

                _context.RiskItemSubClasses.Remove(auditChildModel);
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

        private RiskItemSubClass GetChildAuditModel(AuditModel auditModel)
        {
            return new RiskItemSubClass
            {
                RiskItemSubClassCode = auditModel.Code,
                RiskItemSubClassName = auditModel.Name,
                RiskItemClassCode = auditModel.ParentCode
            };
        }
    }

}
