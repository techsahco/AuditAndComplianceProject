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
    public class RiskItemController : Controller
    {
        private AuditCompliance db = new AuditCompliance();

        // GET: RiskItems
        public ActionResult Index()
        {
            return View(db.RiskItems.ToList());
        }

        // GET: RiskItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskItem riskItem = db.RiskItems.Find(id);
            if (riskItem == null)
            {
                return HttpNotFound();
            }
            return View(riskItem);
        }

        // GET: RiskItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RiskItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(RiskItem riskItem)
        {
            if (ModelState.IsValid)
            {
                var isExist = db.RiskItems.Where(x => x.RiskItemName == riskItem.RiskItemName).FirstOrDefault();
                if (isExist == null)
                {
                    riskItem.CreatedOn = DateTime.Now;
                    riskItem.ModifiedOn = DateTime.Now;
                    db.RiskItems.Add(riskItem);
                    db.SaveChanges();
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false ,errorMesage = "Same Name already exist"}, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: RiskItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskItem riskItem = db.RiskItems.Find(id);
            if (riskItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return Json(new { success = true, data = riskItem }, JsonRequestBehavior.AllowGet);
        }

        // POST: RiskItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(RiskItem riskItem)
        {
            if (ModelState.IsValid)
            {
                RiskItem risk = db.RiskItems.Find(riskItem.RiskItemID);
                if (risk == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    var isExist = db.RiskItems.Where(x => x.RiskItemName == riskItem.RiskItemName && x.RiskItemID != riskItem.RiskItemID).FirstOrDefault();
                    if (isExist == null)
                    {
                        risk.ModifiedOn = DateTime.Now;
                        risk.RiskItemName = riskItem.RiskItemName;
                        risk.Priority = riskItem.Priority;
                        db.SaveChanges();
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { success = false, errorMesage = "Same Name already exist" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: RiskItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiskItem riskItem = db.RiskItems.Find(id);
            if (riskItem == null)
            {
                return HttpNotFound();
            }
            return View(riskItem);
        }

        // POST: RiskItems/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                RiskItem riskItem = db.RiskItems.Find(id);
                if (riskItem != null)
                {
                    db.RiskItems.Remove(riskItem);
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
