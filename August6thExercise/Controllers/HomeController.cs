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
            ViewBag.Roles = new SelectList(allRoles);
            ViewBag.Users = new SelectList(Users);
            return View();
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]

        //public ActionResult AddToRole()
        //{
            
        //}

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