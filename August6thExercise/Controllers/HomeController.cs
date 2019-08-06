using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace August6thExercise.Controllers
{
    public class HomeController : Controller
    {
        private RoleManager<IdentityRole> rolesManger;
        private UserManager<IdentityUser> userManager;

        public HomeController()
        {
            rolesManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
        }
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public ActionResult AddToRole()
        {
            var Users = userManager.Users.ToList();
            var allRoles = rolesManger.Roles.Select(x => x.Name).Distinct().ToList();
            ViewBag.RoleName = new SelectList(allRoles);
            ViewBag.UserId = new SelectList(Users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        public ActionResult AddToRole(string RoleName, string UserId)
        {
            userManager.AddToRole(UserId, RoleName);
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}