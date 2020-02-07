using MVCEvrakTakipSistemi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEvrakTakipSistemi.Controllers
{
    public class KullaniciController : Controller
    {
        MVCEvrakTkipDBEntities entity = new MVCEvrakTkipDBEntities();
        // GET: Kullanici
        public ActionResult Index()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);

            if (yetkiId == 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Olustur()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);

            if (yetkiId == 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Olustur(string evrakAd,System.Web.HttpPostedFileBase yuklenecekDosya)
        {
            if(yuklenecekDosya!=null)
            {
                try
                {
                    string dosyaAd = Path.GetFileName(yuklenecekDosya.FileName);
                    var yuklenmeYeri = Path.Combine(Server.MapPath("~/Evraklar"), dosyaAd);
                    string evrakYol = "/Evraklar/" + dosyaAd;

                    yuklenecekDosya.SaveAs(yuklenmeYeri);

                    int personelId = Convert.ToInt32(Session["personelId"]);

                    Evraklar evrak = new Evraklar();
                    evrak.evrakAd = evrakAd;
                    evrak.perId = personelId;
                    evrak.evrakYol = evrakYol;
                    evrak.evrakTarih = DateTime.Now;
                    evrak.evrakDurumId = 1;
                    evrak.evrakYerId = 1;

                    entity.Evraklar.Add(evrak);
                    entity.SaveChanges();

                    Raporlar rapor = new Raporlar();
                    rapor.evrakId = evrak.evrakId;
                    rapor.personelId = personelId;
                    rapor.tarih = DateTime.Now;
                    rapor.durumId = 1;
                    rapor.yerId = 1;

                    entity.Raporlar.Add(rapor);
                    entity.SaveChanges();

                    return RedirectToAction("Takip", "Kullanici");

                }
                catch (Exception)
                {

                    return RedirectToAction("Index", "Kullanici");
                }
                

            }
            else
            {
                return View();
            }
        }

        public ActionResult Takip()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);
            int personelId = Convert.ToInt32(Session["personelId"]);

            if (yetkiId == 1)
            {
                var evraklar = (from e in entity.Evraklar where e.perId == personelId select e).ToList();
                ViewBag.evraklar = evraklar;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public ActionResult Takip(int selectEvrak)
        {
            var rapor = (from r in entity.Raporlar where r.evrakId == selectEvrak select r).ToList();

            TempData["rapor"] = rapor;

            return RedirectToAction("Liste", "Kullanici");
        }

        public ActionResult Liste()
        {
            List<Raporlar> raporlar = (List<Raporlar>)TempData["rapor"];

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

        public ActionResult Hata()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);
            int personelId = Convert.ToInt32(Session["personelId"]);

            if (yetkiId == 1)
            {
                var evraklar = (from e in entity.Evraklar where e.evrakDurumId == 3 && e.perId == personelId select e).ToList();

                ViewBag.evraklar = evraklar;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Hata(int evrakId)
        {
            var evrak = (from e in entity.Evraklar where e.evrakId == evrakId select e).FirstOrDefault();

            TempData["evrak"] = evrak;
            return RedirectToAction("HataGonder", "Kullanici");
        }

        public ActionResult HataGonder()
        {
            int yetkiId = Convert.ToInt32(Session["yetkiId"]);

            if (yetkiId == 1)
            {
                Evraklar evrak = (Evraklar)TempData["evrak"];

                ViewBag.evrak = evrak;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult HataGonder(int evrakId,string evrakAd,System.Web.HttpPostedFileBase yuklenecekDosya)
        {
            if(yuklenecekDosya!=null)
            {
                try
                {
                    string dosyaAd = Path.GetFileName(yuklenecekDosya.FileName);
                    var yuklenmeYeri = Path.Combine(Server.MapPath("~/Evraklar"), dosyaAd);
                    string evrakYol = "/Evraklar/" + dosyaAd;

                    yuklenecekDosya.SaveAs(yuklenmeYeri);

                    int personelId = Convert.ToInt32(Session["personelId"]);

                    var evrak = (from e in entity.Evraklar where e.evrakId == evrakId select e).FirstOrDefault();
                    evrak.evrakAd = evrakAd;
                    evrak.evrakYol = evrakYol;
                    evrak.evrakDurumId = 2;
                    evrak.evrakYerId = 1;
                    entity.SaveChanges();

                    Raporlar rapor = new Raporlar();
                    rapor.evrakId = evrak.evrakId;
                    rapor.personelId = personelId;
                    rapor.tarih = DateTime.Now;
                    rapor.durumId = 2;
                    rapor.yerId = 1;

                    entity.Raporlar.Add(rapor);
                    entity.SaveChanges();

                    return RedirectToAction("Takip", "Kullanici");

                }
                catch (Exception)
                {

                    return RedirectToAction("Index", "Kullanici");
                }
            }
            else
            {
                try
                {
                    

                    int personelId = Convert.ToInt32(Session["personelId"]);

                    var evrak = (from e in entity.Evraklar where e.evrakId == evrakId select e).FirstOrDefault();
                    evrak.evrakAd = evrakAd;
                    evrak.evrakDurumId = 2;
                    evrak.evrakYerId = 1;
                    entity.SaveChanges();

                    Raporlar rapor = new Raporlar();
                    rapor.evrakId = evrak.evrakId;
                    rapor.personelId = personelId;
                    rapor.tarih = DateTime.Now;
                    rapor.durumId = 2;
                    rapor.yerId = 1;

                    entity.Raporlar.Add(rapor);
                    entity.SaveChanges();

                    return RedirectToAction("Takip", "Kullanici");

                }
                catch (Exception)
                {

                    return RedirectToAction("Index", "Kullanici");
                }
            }
        }
    }
}