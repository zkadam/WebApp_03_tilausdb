using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp_03_tilausdb.Models;

namespace WebApp_03_tilausdb.Controllers
{
    public class AsiakkaatsController : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();

        //------------------------------------------------------------------- GET: Asiakkaats-------------------------------------------------------
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
             var asiakkaat = db.Asiakkaat.Include(a => a.Postitoimipaikat);
            return View(asiakkaat.ToList());

            }
            
           

        }

        // GET: Asiakkaats/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
                if (asiakkaat == null)
                {
                    return HttpNotFound();
                }
                return View(asiakkaat);

            }
            
        }


        // GET: Asiakkaats/--------------------------------------------------------Create------------------------------------------------------
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                List<SelectListItem> NumeroJaPostipaikkaYhistetty = new List<SelectListItem>();
                foreach (Postitoimipaikat NumeroJaPostipaikka in db.Postitoimipaikat)
                {
                    NumeroJaPostipaikkaYhistetty.Add(new SelectListItem
                    {
                        Value = NumeroJaPostipaikka.Postinumero,
                        Text = NumeroJaPostipaikka.Postinumero + " " + NumeroJaPostipaikka.Postitoimipaikka

                    });
                }
                ViewBag.Postinumero = new SelectList(NumeroJaPostipaikkaYhistetty, "Value", "Text");

                return View();

            }
         
        }

        // POST: Asiakkaats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AsiakasID,Nimi,Osoite,Postinumero")] Asiakkaat asiakas)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Asiakkaat.Add(asiakas);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                List<SelectListItem> NumeroJaPostipaikkaYhistetty = new List<SelectListItem>();     //uusi lista jossa posti numero ja kunta on yhdistetty pudotus valikon vartern
                foreach (Postitoimipaikat NumeroJaPostipaikka in db.Postitoimipaikat)
                {
                    NumeroJaPostipaikkaYhistetty.Add(new SelectListItem
                    {
                        Value = NumeroJaPostipaikka.Postinumero,
                        Text = NumeroJaPostipaikka.Postinumero + " " + NumeroJaPostipaikka.Postitoimipaikka

                    });
                }
                ViewBag.Postinumero = new SelectList(NumeroJaPostipaikkaYhistetty, "Value", "Text", asiakas.Postinumero);
                return View(asiakas);

            }
      
        }








        // GET: Asiakkaats/---------------------------------------------------------Edit------------------------------------------------------------
        public ActionResult Edit(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Asiakkaat asiakas = db.Asiakkaat.Find(id);
                if (asiakas == null) return HttpNotFound();
                List<SelectListItem> NumeroJaPostipaikkaYhistetty = new List<SelectListItem>();
                foreach (Postitoimipaikat NumeroJaPostipaikka in db.Postitoimipaikat)
                {
                    NumeroJaPostipaikkaYhistetty.Add(new SelectListItem
                    {
                        Value = NumeroJaPostipaikka.Postinumero,
                        Text = NumeroJaPostipaikka.Postinumero + " " + NumeroJaPostipaikka.Postitoimipaikka

                    });
                }
                ViewBag.Postinumero = new SelectList(NumeroJaPostipaikkaYhistetty, "Value", "Text");
                return View(asiakas);

            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Katso https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult Edit([Bind(Include = "AsiakasID,Nimi,Osoite,Postinumero")] Asiakkaat asiakas)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(asiakas).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                List<SelectListItem> NumeroJaPostipaikkaYhistetty = new List<SelectListItem>();     //uusi lista jossa posti numero ja kunta on yhdistetty pudotus valikon vartern
                foreach (Postitoimipaikat NumeroJaPostipaikka in db.Postitoimipaikat)
                {
                    NumeroJaPostipaikkaYhistetty.Add(new SelectListItem
                    {
                        Value = NumeroJaPostipaikka.Postinumero,
                        Text = NumeroJaPostipaikka.Postinumero + " " + NumeroJaPostipaikka.Postitoimipaikka

                    });
                }
                ViewBag.Postinumero = new SelectList(NumeroJaPostipaikkaYhistetty, "Value", "Text", asiakas.Postinumero);
                return View(asiakas);

            }
           
        }

        // GET: Asiakkaats/-------------------------------------------------------Delete----------------------------------------------
        public ActionResult Delete(int id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
                if (asiakkaat == null)
                {
                    return HttpNotFound();
                }
                return View(asiakkaat);

            }
            
        }

        // POST: Asiakkaats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
                db.Asiakkaat.Remove(asiakkaat);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            
        }
        //---------------------------------------------------------------------------database Dispose----------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
