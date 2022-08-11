using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models.Entity;

namespace WebApplication4.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.TBLURUNLER select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.URUNADI.Contains(p));
            }
            //var degerler = db.TBLURUNLER.ToList();
            //var degerler = db.TBLURUNLER.ToList().ToPagedList(sayfa, 4);
            //return View(degerler);
            return View(degerler.ToList());
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,

                                                 Value=i.KATEGORIID.ToString()

                                           }).ToList();
            ViewBag.dgr = degerler;
                                          

            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
         public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,

                                                 Value = i.KATEGORIID.ToString()

                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(TBLURUNLER urun)
        {
            var yeniUrun = db.TBLURUNLER.Where(x => x.URUNID == urun.URUNID).FirstOrDefault();
            yeniUrun.URUNADI = urun.URUNADI;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    
    }
}