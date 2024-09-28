using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMVC.Models.Entity;
using System.Web.Security;

namespace StokMVC.Controllers
{
    public class GirisYapController : Controller
    {

        dbSTOKEntities db = new dbSTOKEntities();
        // GET: GirisYap
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(tblADMIN t)
        {
            var bilgiler = db.tblADMIN.FirstOrDefault(x => x.kullanici == t.kullanici && x.sifre == t.sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.kullanici, false);
                return RedirectToAction("Index", "Musteri");
            }
            else
            {
                return View();

            }
        }
    }
}