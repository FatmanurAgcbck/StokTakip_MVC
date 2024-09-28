using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMVC.Models.Entity;

namespace StokMVC.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori

        dbSTOKEntities db = new dbSTOKEntities();

        public ActionResult Index()
        {
            var kategoriler = db.tblKATEGORI.ToList();

            return View(kategoriler);
        }

        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult YeniKategori(tblKATEGORI p)
        {
            db.tblKATEGORI.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var ktg = db.tblKATEGORI.Find(id);
            db.tblKATEGORI.Remove(ktg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.tblKATEGORI.Find(id);
            return View("KategoriGetir", ktg);

        }

        public ActionResult Guncelle(tblKATEGORI p)
        {
            var ktg = db.tblKATEGORI.Find(p.id);
            ktg.ad = p.ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}