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
    public class AuditRiskItemController : Controller
    {
        private readonly AuditCompliance _context = new AuditCompliance();

        // GET: AuditClass
        public ActionResult Index()
        {
            ViewBag.PrimaryDepartments = _context.Departments.Where(x => x.IsActive == true && x.IsPrimary == true).ToList();
            ViewBag.SecondaryDepartments = _context.Departments.Where(x => x.IsActive == true && x.IsPrimary == false).ToList();
            return View(new GenericAuditViewModel<AuditRiskItem>
            {
                AuditModel = _context.AuditRiskItems
                .Include(a => a.RiskItemSubClass)
                .Include(a => a.RiskItemSubClass.RiskItemClass)
                .Include(a => a.RiskItemSubClass.RiskItemClass.RiskItemCategory)
                .Include(a => a.RiskItemSubClass.RiskItemClass.RiskItemCategory.AuditClass)
                .Include(a => a.RiskItemSubClass.RiskItemClass.RiskItemCategory.AuditClass.AuditCategory)
                .ToList(),
                ParentAuditDataModel = _context.RiskItemSubClasses.Select(x => new AuditModel { Code = x.RiskItemSubClassCode, Name = x.RiskItemSubClassName }).ToList()
            });
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(AuditRiskItemModel auditModel)
        {
            var isExist = _context.AuditRiskItems.Where(x => x.RiskItemCode == auditModel.Code).FirstOrDefault();

            if (isExist != null) return Json(new { success = false, errorMesage = "Same Code already exist" }, JsonRequestBehavior.AllowGet);
            var riskItemDbModel = GetChildAuditModel(auditModel);
            _context.AuditRiskItems.Add(riskItemDbModel);
            _context.SaveChanges();
            if(auditModel.SecondaryDepartment.Count() > 0)
            {
                foreach (var item in auditModel.SecondaryDepartment)
                {
                    var secondaryRelation = new AuditRiskItemSecondaryDepartment() { AuditRiskItemCode = riskItemDbModel.RiskItemCode, DepartmentId = Convert.ToInt32(item) };
                    _context.AuditRiskItemSecondaryDepartment.Add(secondaryRelation);
                    _context.SaveChanges();
                }
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: auditChildModel/Edit/5
        public ActionResult Edit(string code)
        {
            if (code == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var auditChildModel = _context.AuditRiskItems.Find(code);

            if (auditChildModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return Json(new
            {
                success = true,
                data = new AuditRiskItemModel
                {
                    Code = auditChildModel.RiskItemCode,
                    Name = auditChildModel.RiskItemName,
                    ParentCode = auditChildModel.RiskItemSubClassCode,
                    PrimaryDepartment = auditChildModel.PrimaryDepartment.ToString(),
                    SecondaryDepartment = _context.AuditRiskItemSecondaryDepartment.Where(x => x.AuditRiskItemCode == code).Select(x => x.DepartmentId.ToString()).ToList()
                }
            }, JsonRequestBehavior.AllowGet) ;
        }

        // POST: auditChildModel/Edit/5
        [HttpPost]
        public ActionResult Edit(AuditRiskItemModel auditModel)
        {
            if (auditModel.Code == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var auditChildModel = _context.AuditRiskItems.Find(auditModel.Code);

            if (auditChildModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            auditChildModel.RiskItemName = auditModel.Name;
            auditChildModel.RiskItemSubClassCode = auditModel.ParentCode;
            _context.Entry(auditChildModel).State = EntityState.Modified;
            _context.SaveChanges();
            if (auditModel.SecondaryDepartment.Count() > 0)
            {
                var allSecondary = _context.AuditRiskItemSecondaryDepartment.Where(x => x.AuditRiskItemCode == auditModel.Code).ToList();
                _context.AuditRiskItemSecondaryDepartment.RemoveRange(allSecondary);
                _context.SaveChanges();
                foreach (var item in auditModel.SecondaryDepartment)
                {
                    var secondaryRelation = new AuditRiskItemSecondaryDepartment() { AuditRiskItemCode = auditModel.Code, DepartmentId = Convert.ToInt32(item) };
                    _context.AuditRiskItemSecondaryDepartment.Add(secondaryRelation);
                    _context.SaveChanges();
                }
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // POST: AuditCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string code)
        {
            try
            {
                var auditChildModel = _context.AuditRiskItems.Find(code);
                if (auditChildModel == null) return Json(new { success = false }, JsonRequestBehavior.AllowGet);

                _context.AuditRiskItems.Remove(auditChildModel);
                _context.SaveChanges();
                var allSecondary = _context.AuditRiskItemSecondaryDepartment.Where(x => x.AuditRiskItemCode == code).ToList();
                _context.AuditRiskItemSecondaryDepartment.RemoveRange(allSecondary);
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

        private AuditRiskItem GetChildAuditModel(AuditRiskItemModel auditModel)
        {
            return new AuditRiskItem
            {
                RiskItemCode = auditModel.Code,
                RiskItemName = auditModel.Name,
                RiskItemSubClassCode = auditModel.ParentCode,
                PrimaryDepartment = Convert.ToInt32(auditModel.PrimaryDepartment),
            };
        }
    }
}
