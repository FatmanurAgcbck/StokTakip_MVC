using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using StokMVC.Models.Entity;

namespace StokMVC.Controllers
{
    public class SatislarController : Controller
    {

        dbSTOKEntities db = new dbSTOKEntities();
        // GET: Satislar
        public ActionResult Index()
        {
            var satislar = db.tblSATISLAR.ToList();
            return View(satislar);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            //ürünler
            List<SelectListItem> urun = (from x in db.tblURUNLER.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString()
                                         }).ToList();
            ViewBag.drop1 = urun;


            //personel
            List<SelectListItem> personel = (from x in db.tblPERSONEL.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.ad + " " + x.soyad,
                                                 Value = x.id.ToString()
                                             }).ToList();
            ViewBag.drop2 = personel;

            //Müşteriler 
            List<SelectListItem> musteri = (from x in db.tblMUSTERI.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.ad + " " + x.soyad,
                                                Value = x.id.ToString()
                                            }).ToList();
            ViewBag.drop3 = musteri;

            //fiyat
            List<SelectListItem> fiyat = (from x in db.tblURUNLER.ToList()
                                          select new SelectListItem
                                          {

                                              Text = Convert.ToString(x.satisFiyat),
                                              Value = x.id.ToString()
                                          }).ToList();


            ViewBag.drop4 = fiyat;


            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(tblSATISLAR p)
        {
            var urun = db.tblURUNLER.Where(x => x.id == p.tblURUNLER.id).FirstOrDefault();
            var musteri = db.tblMUSTERI.Where(x => x.id == p.tblMUSTERI.id).FirstOrDefault();
            var personel = db.tblPERSONEL.Where(x => x.id == p.tblPERSONEL.id).FirstOrDefault();
            p.tblMUSTERI = musteri;
            p.tblPERSONEL = personel;
            p.tblURUNLER = urun;

            p.tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.tblSATISLAR.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}