using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using August6thExercise.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace August6thExercise.Controllers
{
    public class ProjectsController : Controller
    {
        private RoleManager<IdentityRole> rolesManger;
        private UserManager<IdentityUser> userManager;
        ProjectHelper ph;

        public ProjectsController()
        {
            rolesManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            ph = new ProjectHelper(db);
        }

        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Projects
        [Authorize(Roles = "Admin, ProjectManager, Developer, Submitter")]
        public ActionResult Index()
        {
            //Displaying The List Of Projects For Each Indvidual User 
            if (User.IsInRole("Admin") || User.IsInRole("Project Manager"))
            {
                return View(db.Projects.ToList());
            }
            else
            {
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                var assignedProjects = user.Projects.ToList();
                return View(assignedProjects);
            }
            
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectPlan,StartDate,DueDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectPlan,StartDate,DueDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
            
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult AssignProject()
        {
            var Users = userManager.Users.ToList();
            var allProjects = db.Projects.ToList();
            ViewBag.projectId = new SelectList(allProjects, "Id", "ProjectPlan");
            ViewBag.UserId = new SelectList(Users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        public ActionResult AssignProject(int projectId, string UserId)
        {
            var user = db.Users.Find(UserId);
            var project = db.Projects.Find(projectId);
            user.Projects.Add(project);
            db.SaveChanges();
            return View();
        }

        public ActionResult EmptyProjects()
        {
            var empty = ph.emptyProject();
            return View(empty);
        }

        public ActionResult isUserOnProject(string userId, int projectId)
        {
            bool project = ph.isUserOnProject(userId, projectId);
            ViewBag.project = project;
            return View();
        }
        
        public ActionResult GetUserProjects()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var project = ph.GetUserProjects(userId);
                return View(project);
            }
            else
            {
                ViewBag.Message = "Please Login";
                return View();
            }
        }
        public ActionResult AssignToProject()
        {
            var Users = userManager.Users.ToList();
            var allProjects = db.Projects.ToList();
            ViewBag.projectId = new SelectList(allProjects, "Id", "ProjectPlan");
            ViewBag.UserId = new SelectList(Users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        public ActionResult AssignToProject(string userId, int projectId)
        {
            var user = db.Users.Find(userId);
            var project = db.Projects.Find(projectId);
            ph.CheckUserId(user.Id);
            if(!ph.isUserOnProject(userId, projectId))
            {
                ph.assignToProject(userId, projectId);
            }
            return RedirectToAction("Index");
        }

    }
}
