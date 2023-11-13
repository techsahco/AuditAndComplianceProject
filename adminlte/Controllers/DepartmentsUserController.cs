using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using adminlte.Models;
using adminlte.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace adminlte.Controllers
{
    public class DepartmentsUserController : Controller
    {
        private AuditCompliance db = new AuditCompliance();
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }

            private set
            {
                _roleManager = value;
            }
        }

        // GET: Departments
        public ActionResult Index()
        {
            var departments = db.Departments.ToList();
            var departUsers = db.DepartmentUser.ToList();
            List<DepartmentUserViewModel> list = new List<DepartmentUserViewModel>();
            foreach (var item in departUsers)
            {
                list.Add(new DepartmentUserViewModel()
                {
                    DepartmentName = departments.Where(x => x.DepartmentID == item.DepartmentID).FirstOrDefault().DepartmentName,
                    UserName = UserManager.FindByIdAsync(item.UserId).Result.UserName,
                    UserId = item.UserId,

                });
            }
            return View(list);
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
        public async Task<ActionResult> Create(DepartmentUserRequestViewModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return View(requestModel);
            }
            else
            {
                var user = new ApplicationUser { UserName = requestModel.Email, Email = requestModel.Email };
                var userEmailExist = UserManager.FindByEmail(user.Email);
                if (userEmailExist == null)
                {
                    var result = await UserManager.CreateAsync(user, requestModel.Password);
                    if (result.Succeeded)
                    {
                        // Find the role you want to assign (e.g., "Admin")
                        var role = await RoleManager.FindByNameAsync("User");

                        if (role != null)
                        {
                            // Assign the role to the user
                            var roleResult = await UserManager.AddToRoleAsync(user.Id, role.Name);

                            if (roleResult.Succeeded)
                            {
                                //now add relation to Department User table
                                db.DepartmentUser.Add(new DepartmentUser() { UserId = user.Id, DepartmentID = requestModel.DepartmentID, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now });
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                // Handle role assignment error
                                ModelState.AddModelError(string.Empty, "Error assigning role to the user.");
                                return View(requestModel);
                            }
                        }
                        else
                        {
                            // Handle role not found error
                            ModelState.AddModelError(string.Empty, "Selected role not found.");
                            return View(requestModel);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User Exist with same email");
                }

                return View(requestModel);
            }
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentUserRequestViewModel departmentUserRequestViewModel = new DepartmentUserRequestViewModel();
            DepartmentUser depUser = db.DepartmentUser.Where(x => x.UserId == id).FirstOrDefault();
            var department = db.Departments.Where(x => x.DepartmentID == depUser.DepartmentID).FirstOrDefault();
            var user = UserManager.FindById(depUser.UserId);
            departmentUserRequestViewModel.Email = user.Email;
            departmentUserRequestViewModel.userId = user.Id;
            departmentUserRequestViewModel.DepartmentID = department.DepartmentID;
            if (depUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.selectedDepartment = department.DepartmentID;
            return View(departmentUserRequestViewModel);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(DepartmentUserRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByEmail(model.Email);
                var departmentUser = db.DepartmentUser.Where(x => x.UserId == user.Id && x.DepartmentID == model.DepartmentID).FirstOrDefault();
                
                if (departmentUser == null)
                {
                    ModelState.AddModelError(null, "Unable to find user");
                }
                else
                {
                    departmentUser.ModifiedOn = DateTime.Now;
                    db.SaveChanges();
                    if(user != null) {
                        UserManager.ChangePassword(user.Id, model.OldPassword, model.Password);
                    }
                }
            }
            ViewBag.selectedDepartment = model.DepartmentID;
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> VerifyPassword(string email, string oldpassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(oldpassword))
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var isVerfied = await UserManager.CheckPasswordAsync(UserManager.FindByEmail(email), oldpassword);
                if (isVerfied)
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

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
        public ActionResult DeleteConfirmed(string userid)
        {
            try
            {
                var user = UserManager.FindById(userid);
                var departmentUser = db.DepartmentUser.Where(x => x.UserId == user.Id).FirstOrDefault();

                if (departmentUser != null)
                {
                    //remove relation
                    db.DepartmentUser.Remove(departmentUser);
                    db.SaveChanges();
                    //remove user
                    UserManager.Delete(user);
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
