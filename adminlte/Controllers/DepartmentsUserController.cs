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
    public class DepartmentsUserController : Controller
    {                                                                                   
        private AuditCompliance db = new AuditCompliance();

        // GET: Departments
        public ActionResult Index()
        {

            return View(db.Departments.ToList());
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                var isExist = db.Departments.Where(x => x.DepartmentName == department.DepartmentName).FirstOrDefault();
                if (isExist == null)
                {
                    department.CreatedOn = DateTime.Now;
                    department.ModifiedOn = DateTime.Now;
                    db.Departments.Add(department);
                    db.SaveChanges();
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false ,errorMesage = "Same Name already exist"}, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return Json(new { success = true, data = department }, JsonRequestBehavior.AllowGet);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                Department dep = db.Departments.Find(department.DepartmentID);
                if (dep == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    var isExist = db.Departments.Where(x => x.DepartmentName == department.DepartmentName && x.DepartmentID != department.DepartmentID).FirstOrDefault();
                    if (isExist == null)
                    {
                        dep.ModifiedOn = DateTime.Now;
                        dep.DepartmentName = department.DepartmentName;
                        dep.IsPrimary = department.IsPrimary;
                        db.SaveChanges();
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { success = false, errorMesage = "Same Name already exist" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Department department = db.Departments.Find(id);
                if (department != null)
                {
                    db.Departments.Remove(department);
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
