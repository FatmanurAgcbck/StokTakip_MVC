using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMVC.Models.Entity;

namespace StokMVC.Controllers
{
    public class UrunController : Controller
    {

        dbSTOKEntities db = new dbSTOKEntities();
        // GET: Urun
        public ActionResult Index(string p)
        {
            //var urunler = db.tblURUNLER.Where(x => x.durum == true).ToList(); 
            var urunler = db.tblURUNLER.Where(x => x.durum == true);
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.ad.Contains(p) && x.durum == true);
            }
            return View(urunler.ToList());
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktg = (from x in db.tblKATEGORI.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.drop = ktg;

            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(tblURUNLER p)
        {
            var ktgr = db.tblKATEGORI.Where(x => x.id == p.tblKATEGORI.id).FirstOrDefault();
            p.tblKATEGORI = ktgr;
            db.tblURUNLER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> urun = (from x in db.tblKATEGORI.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString()
                                         }).ToList();
            var urn = db.tblURUNLER.Find(id);
            ViewBag.urunKategori = urun;
            return View("UrunGetir", urn);
        }

        public ActionResult Guncelle(tblURUNLER p)
        {
            var urun = db.tblURUNLER.Find(p.id);
            urun.marka = p.marka;
            urun.ad = p.ad;
            urun.stok = p.stok;
            urun.alisFiyat = p.alisFiyat;
            urun.satisFiyat = p.satisFiyat;
            var ktg = db.tblKATEGORI.Where(x => x.id == p.tblKATEGORI.id).FirstOrDefault();
            urun.kategori = ktg.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(tblURUNLER p)
        {
            var urunBul = db.tblURUNLER.Find(p.id);
            urunBul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}