using MVCEvrakTakipSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEvrakTakipSistemi.Controllers
{
    public class OnMaliController : Controller
    {
        MVCEvrakTkipDBEntities entity = new MVCEvrakTkipDBEntities();
        // GET: OnMali
        public ActionResult Index()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);

            if (yetkiId == 2)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

        public ActionResult Bekleyen()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);

            if (yetkiId == 2)
            {
                var evraklar = (from e in entity.Evraklar where e.evrakDurumId == 1 && e.evrakYerId == 1 select e).ToList();

                ViewBag.evraklar = evraklar;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Bekleyen(int evrakId)
        {
            int personelId = Convert.ToInt32(Session["personelId"]);

            var evrak = (from e in entity.Evraklar where e.evrakId == evrakId select e).FirstOrDefault();

            evrak.evrakDurumId = 2;
            entity.SaveChanges();

            Raporlar rapor = new Raporlar();

            rapor.personelId = personelId;
            rapor.durumId = 2;
            rapor.yerId = 1;
            rapor.tarih = DateTime.Now;
            rapor.evrakId = evrak.evrakId;

            entity.Raporlar.Add(rapor);
            entity.SaveChanges();

            return RedirectToAction("Incelenen", "OnMali");
        }

        public ActionResult Incelenen()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);

            if (yetkiId == 2)
            {
                var evraklar = (from e in entity.Evraklar where e.evrakDurumId == 2 && e.evrakYerId == 1 select e).ToList();

                ViewBag.evraklar = evraklar;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Incelenen(string btnBasarili,string btnHatali,int evrakId)
        {
            int personelId = Convert.ToInt32(Session["personelId"]);
            if (btnBasarili != null)
            {
                var evrak = (from e in entity.Evraklar where e.evrakId == evrakId select e).FirstOrDefault();

                evrak.evrakDurumId = 2;
                evrak.evrakYerId = 2;
                entity.SaveChanges();

                Raporlar rapor = new Raporlar();

                rapor.personelId = personelId;
                rapor.durumId = 2;
                rapor.yerId = 2;
                rapor.tarih = DateTime.Now;
                rapor.evrakId = evrak.evrakId;

                entity.Raporlar.Add(rapor);
                entity.SaveChanges();

                return RedirectToAction("Index", "OnMali");
            }
            if (btnHatali != null)
            {
                var evrak = (from e in entity.Evraklar where e.evrakId == evrakId select e).FirstOrDefault();

                evrak.evrakDurumId = 3;
                entity.SaveChanges();

                Raporlar rapor = new Raporlar();

                rapor.personelId = personelId;
                rapor.durumId = 3;
                rapor.yerId = 1;
                rapor.tarih = DateTime.Now;
                rapor.evrakId = evrak.evrakId;

                entity.Raporlar.Add(rapor);
                entity.SaveChanges();

                return RedirectToAction("Hata", "OnMali");
            }

            return View();
            
        }

        public ActionResult Hata()
        {
            var raporlar = (from r in entity.Raporlar where r.durumId == 3 && r.yerId == 1 select r).ToList();

            List<AyrintiliRapor> list = new List<AyrintiliRapor>();

            foreach (var item in raporlar)
            {
                AyrintiliRapor ar = new AyrintiliRapor();

                ar.evrakId = item.Evraklar.evrakId;
                ar.evrakAd = item.Evraklar.evrakAd;
                ar.tarih = Convert.ToDateTime(item.tarih);
                ar.personelAd = item.Personeller.perAd;
                ar.yerAd = item.Yerler.yerAd;
                ar.durumAd = item.Durumlar.durumAd;

                list.Add(ar);

            }

            ViewBag.raporlar = list;
            return View();
        }

        public ActionResult Basarili()
        {
            var raporlar = (from r in entity.Raporlar where r.durumId == 2 && r.yerId == 2 select r).ToList();

            List<AyrintiliRapor> list = new List<AyrintiliRapor>();

            foreach (var item in raporlar)
            {
                AyrintiliRapor ar = new AyrintiliRapor();

                ar.evrakId = item.Evraklar.evrakId;
                ar.evrakAd = item.Evraklar.evrakAd;
                ar.tarih = Convert.ToDateTime(item.tarih);
                ar.personelAd = item.Personeller.perAd;
                ar.yerAd = item.Yerler.yerAd;
                ar.durumAd = item.Durumlar.durumAd;

                list.Add(ar);

            }

            ViewBag.raporlar = list;
            return View();
        }

    }
}