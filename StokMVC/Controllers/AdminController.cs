using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMVC.Models.Entity;

namespace StokMVC.Controllers
{
    public class AdminController : Controller
    {

        dbSTOKEntities db = new dbSTOKEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniAdmin(tblADMIN p)
        {
            db.tblADMIN.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}