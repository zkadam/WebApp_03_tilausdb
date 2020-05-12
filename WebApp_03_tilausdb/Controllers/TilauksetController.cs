using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp_03_tilausdb.Models;

namespace WebApp_03_tilausdb.Controllers
{
    public class TilauksetController : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();
        // GET: Tilaukset---------------------------------------------------index---------------------------------------------
        public ActionResult Index()
        {
            var tilauskset = db.Tilaukset;
            return View(tilauskset.ToList());
            
        }
        //--------------------------------------------------------------create---------------------------------------------------------
        public ActionResult Create()
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
            
            List<SelectListItem> AsiakasIDjaNimiYhistetty = new List<SelectListItem>();
            foreach (Asiakkaat IDjaNimi in db.Asiakkaat)
            {
                AsiakasIDjaNimiYhistetty.Add(new SelectListItem
                {
                    Value = IDjaNimi.AsiakasID.ToString(),
                    Text = IDjaNimi.AsiakasID.ToString() + " " + IDjaNimi.Nimi

                });
            }



            ViewBag.Postinumero = new SelectList(NumeroJaPostipaikkaYhistetty, "Value", "Text" );
            ViewBag.AsiakasID = new SelectList(AsiakasIDjaNimiYhistetty,"Value", "Text");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset uusiTilaus)
        {


            if (ModelState.IsValid)
            {
                db.Tilaukset.Add(uusiTilaus);
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
            List<SelectListItem> AsiakasIDjaNimiYhistetty = new List<SelectListItem>();         //uusi lista jossa AsiakasID ja Nimi on yhdistetty pudotus valikon vartern
            foreach (Asiakkaat IDjaNimi in db.Asiakkaat)
            {
                AsiakasIDjaNimiYhistetty.Add(new SelectListItem
                {
                    Value = IDjaNimi.AsiakasID.ToString(),
                    Text = IDjaNimi.AsiakasID.ToString() + " " + IDjaNimi.Nimi

                });
            }



            ViewBag.Postinumero = new SelectList(NumeroJaPostipaikkaYhistetty, "Value", "Text",uusiTilaus.Postinumero);
            ViewBag.AsiakasID = new SelectList(AsiakasIDjaNimiYhistetty, "Value", "Text", uusiTilaus.AsiakasID);
            return View(uusiTilaus);
        }

        //---------------------------------------------------------------------EDIT---------------------------------------------------------------------------------------
        public ActionResult Edit(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tilaukset muokattavaTilaus = db.Tilaukset.Find(id);
            if (id == null) return HttpNotFound();
            List<SelectListItem> NumeroJaPostipaikkaYhistetty = new List<SelectListItem>();
            foreach (Postitoimipaikat NumeroJaPostipaikka in db.Postitoimipaikat)
            {
                NumeroJaPostipaikkaYhistetty.Add(new SelectListItem
                {
                    
                
                    Value = NumeroJaPostipaikka.Postinumero,
                    Text = NumeroJaPostipaikka.Postinumero + " " + NumeroJaPostipaikka.Postitoimipaikka,
                  
                   
                });
            }

            List<SelectListItem> AsiakasIDjaNimiYhistetty = new List<SelectListItem>();
            foreach (Asiakkaat IDjaNimi in db.Asiakkaat)
            {
                AsiakasIDjaNimiYhistetty.Add(new SelectListItem
                {
                    Value = IDjaNimi.AsiakasID.ToString(),
                    Text = IDjaNimi.AsiakasID.ToString() + " " + IDjaNimi.Nimi

                });
            }



            ViewBag.Postinumero = new SelectList(NumeroJaPostipaikkaYhistetty, "Value", "Text");
            ViewBag.AsiakasID = new SelectList(AsiakasIDjaNimiYhistetty, "Value", "Text");
            return View(muokattavaTilaus);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset muokattavaTilaus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(muokattavaTilaus).State = EntityState.Modified;
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
            List<SelectListItem> AsiakasIDjaNimiYhistetty = new List<SelectListItem>();         //uusi lista jossa AsiakasID ja Nimi on yhdistetty pudotus valikon vartern
            foreach (Asiakkaat IDjaNimi in db.Asiakkaat)
            {
                AsiakasIDjaNimiYhistetty.Add(new SelectListItem
                {
                    Value = IDjaNimi.AsiakasID.ToString(),
                    Text = IDjaNimi.AsiakasID.ToString() + " " + IDjaNimi.Nimi

                });
            }



            ViewBag.Postinumero = new SelectList(NumeroJaPostipaikkaYhistetty, "Value", "Text", muokattavaTilaus.Postinumero);
            ViewBag.AsiakasID = new SelectList(AsiakasIDjaNimiYhistetty, "Value", "Text",muokattavaTilaus.AsiakasID);
            return View(muokattavaTilaus);
        }

//----------------------------------------------------------------------------------------DELETE---------------------------------------------------------------------------
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilaukset tilaus = db.Tilaukset.Find(id);
            if (tilaus == null)
            {
                return HttpNotFound();
            }
            return View(tilaus);
        }

        // POST: Asiakkaats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tilaukset tilaus = db.Tilaukset.Find(id);
            db.Tilaukset.Remove(tilaus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



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