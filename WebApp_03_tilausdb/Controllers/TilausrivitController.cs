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
    public class TilausrivitController : Controller
    {
        // GET: Tilausrivit
        private TilausDBEntities db = new TilausDBEntities();
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                List<Tilausrivit> tilausrivit = db.Tilausrivit.ToList();
                return PartialView(tilausrivit);

            }
           
        }


        //----------------------------------------------------------CREATE--------------------------------------------------------------------
        // GET: Tuotteets/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                List<SelectListItem> TilausID = new List<SelectListItem>();                     //TILAUS ID DROPDOWN
                foreach (Tilaukset ID in db.Tilaukset)
                {
                    TilausID.Add(new SelectListItem
                    {
                        Value = ID.TilausID.ToString(),
                        Text = ID.TilausID.ToString()

                    });
                }

                List<SelectListItem> TilausRiviID = new List<SelectListItem>();                 //TILAUSRIVI ID DROPDOWN
                foreach (Tilausrivit ID in db.Tilausrivit)
                {
                    TilausID.Add(new SelectListItem
                    {
                        Value = ID.TilausriviID.ToString(),
                        Text = ID.TilausriviID.ToString()

                    });
                }

                List<SelectListItem> TuoteID = new List<SelectListItem>();                      //TUOTE ID DROPDOWN
                foreach (Tuotteet ID in db.Tuotteet)
                {
                    TuoteID.Add(new SelectListItem
                    {
                        Value = ID.TuoteID.ToString(),
                        Text = ID.TuoteID.ToString() + " " + ID.Nimi

                    });
                }

                ViewBag.TilausID = new SelectList(TilausID, "Value", "Text");
                ViewBag.TilausriviID = new SelectList(TilausRiviID, "Value", "Text");
                ViewBag.TuoteID = new SelectList(TuoteID, "Value", "Text");

                return View();

            }
           
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausriviID,TilausID,TuoteID,Maara,Ahinta")] Tilausrivit tilausrivi)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Tilausrivit.Add(tilausrivi);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                List<SelectListItem> TilausID = new List<SelectListItem>();                     //TILAUS ID DROPDOWN
                foreach (Tilaukset ID in db.Tilaukset)
                {
                    TilausID.Add(new SelectListItem
                    {
                        Value = ID.TilausID.ToString(),
                        Text = ID.TilausID.ToString()

                    });
                }

                List<SelectListItem> TilausRiviID = new List<SelectListItem>();                 //TILAUSRIVI ID DROPDOWN
                foreach (Tilausrivit ID in db.Tilausrivit)
                {
                    TilausID.Add(new SelectListItem
                    {
                        Value = ID.TilausriviID.ToString(),
                        Text = ID.TilausriviID.ToString()

                    });
                }

                List<SelectListItem> TuoteID = new List<SelectListItem>();                      //TUOTE ID DROPDOWN
                foreach (Tuotteet ID in db.Tuotteet)
                {
                    TuoteID.Add(new SelectListItem
                    {
                        Value = ID.TuoteID.ToString(),
                        Text = ID.TuoteID.ToString() + " " + ID.Nimi

                    });
                }

                ViewBag.TilausID = new SelectList(TilausID, "Value", "Text", tilausrivi.TilausID);
                ViewBag.TilausriviID = new SelectList(TilausRiviID, "Value", "Text", tilausrivi.TilausriviID);
                ViewBag.TuoteID = new SelectList(TuoteID, "Value", "Text", tilausrivi.TuoteID);

                return View();

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
                Tilausrivit tilausrivi = db.Tilausrivit.Find(id);
                if (tilausrivi == null) return HttpNotFound();
                List<SelectListItem> TilausID = new List<SelectListItem>();                     //TILAUS ID DROPDOWN
                foreach (Tilaukset ID in db.Tilaukset)
                {
                    TilausID.Add(new SelectListItem
                    {
                        Value = ID.TilausID.ToString(),
                        Text = ID.TilausID.ToString()

                    });
                }

                List<SelectListItem> TilausRiviID = new List<SelectListItem>();                 //TILAUSRIVI ID DROPDOWN
                foreach (Tilausrivit ID in db.Tilausrivit)
                {
                    TilausID.Add(new SelectListItem
                    {
                        Value = ID.TilausriviID.ToString(),
                        Text = ID.TilausriviID.ToString()

                    });
                }

                List<SelectListItem> TuoteID = new List<SelectListItem>();                      //TUOTE ID DROPDOWN
                foreach (Tuotteet ID in db.Tuotteet)
                {
                    TuoteID.Add(new SelectListItem
                    {
                        Value = ID.TuoteID.ToString(),
                        Text = ID.TuoteID.ToString() + " " + ID.Nimi

                    });
                }

                ViewBag.TilausID = new SelectList(TilausID, "Value", "Text");
                ViewBag.TilausriviID = new SelectList(TilausRiviID, "Value", "Text");
                ViewBag.TuoteID = new SelectList(TuoteID, "Value", "Text");
                return View(tilausrivi);

            }
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Katso https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult Edit([Bind(Include = "TilausriviID,TilausID,TuoteID,Maara,Ahinta")] Tilausrivit tilausrivi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tilausrivi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> TilausID = new List<SelectListItem>();                     //TILAUS ID DROPDOWN
            foreach (Tilaukset ID in db.Tilaukset)
            {
                TilausID.Add(new SelectListItem
                {
                    Value = ID.TilausID.ToString(),
                    Text = ID.TilausID.ToString()

                });
            }

            List<SelectListItem> TilausRiviID = new List<SelectListItem>();                 //TILAUSRIVI ID DROPDOWN
            foreach (Tilausrivit ID in db.Tilausrivit)
            {
                TilausID.Add(new SelectListItem
                {
                    Value = ID.TilausriviID.ToString(),
                    Text = ID.TilausriviID.ToString()

                });
            }

            List<SelectListItem> TuoteID = new List<SelectListItem>();                      //TUOTE ID DROPDOWN
            foreach (Tuotteet ID in db.Tuotteet)
            {
                TuoteID.Add(new SelectListItem
                {
                    Value = ID.TuoteID.ToString(),
                    Text = ID.TuoteID.ToString() + " " + ID.Nimi

                });
            }

            ViewBag.TilausID = new SelectList(TilausID, "Value", "Text", tilausrivi.TilausID);
            ViewBag.TilausriviID = new SelectList(TilausRiviID, "Value", "Text", tilausrivi.TilausriviID);
            ViewBag.TuoteID = new SelectList(TuoteID, "Value", "Text", tilausrivi.TuoteID);
            
            return View(tilausrivi);
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
                Tilausrivit tilausrivi = db.Tilausrivit.Find(id);
                if (tilausrivi == null)
                {
                    return HttpNotFound();
                }
                return View(tilausrivi);

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
                Tilausrivit tilausrivi = db.Tilausrivit.Find(id);
                db.Tilausrivit.Remove(tilausrivi);
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