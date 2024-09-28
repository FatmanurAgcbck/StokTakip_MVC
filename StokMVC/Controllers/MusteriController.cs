using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMVC.Models.Entity;
using PagedList;
using PagedList.Mvc;
using System.Drawing.Printing;

namespace StokMVC.Controllers
{
    public class MusteriController : Controller
    {

        dbSTOKEntities db = new dbSTOKEntities();
        [Authorize]
        // GET: Musteri
        public ActionResult Index(int sayfa = 1)
        {

            //var musteriler = db.tblMUSTERI.ToList();
            var musteriler = db.tblMUSTERI.Where(x=>x.durum == true).ToList().ToPagedList(sayfa, 10);
            return View(musteriler);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(tblMUSTERI p)
        {

            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            p.durum = true;
            db.tblMUSTERI.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Sil(tblKATEGORI p)
        {
            var musteri = db.tblMUSTERI.Find(p.id);
            musteri.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var mst = db.tblMUSTERI.Find(id);
            return View("MusteriGetir", mst);
        }

        public ActionResult Guncelle(tblMUSTERI p)
        {
            var mst = db.tblMUSTERI.Find(p.id);
            mst.ad = p.ad;
            mst.soyad = p.soyad;
            mst.sehir = p.sehir;
            mst.bakiye = p.bakiye;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}