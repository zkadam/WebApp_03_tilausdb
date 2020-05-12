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
    public class TuotteetsController : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();

        //----------------------------------------------------------INDEX-------------------------------------------------------------------
        public ActionResult Index()
        {
            return View(db.Tuotteet.ToList());
        }
        //----------------------------------------------------------PRODCARDS-------------------------------------------------------------------

        public ActionResult ProdCards()
        {
            
            return View(db.Tuotteet.ToList());
        }


        //----------------------------------------------------------CREATE-------------------------------------------------------------------
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                return View();

            }
           
        }

        // POST: Tuotteets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TuoteID,Nimi,Ahinta,Kuvalinkki")] Tuotteet tuotteet)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Tuotteet.Add(tuotteet);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(tuotteet);

            }
         
        }

        //----------------------------------------------------------EDIT-------------------------------------------------------------------
        public ActionResult Edit(int? id)
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
                Tuotteet tuotteet = db.Tuotteet.Find(id);
                if (tuotteet == null)
                {
                    return HttpNotFound();
                }
                return View(tuotteet);

            }
           
        }

        // POST: Tuotteets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TuoteID,Nimi,Ahinta,Kuvalinkki")] Tuotteet tuotteet)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tuotteet).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(tuotteet);

            }
           
        }

        //----------------------------------------------------------DELETE-------------------------------------------------------------------

        public ActionResult Delete(int? id)
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
                Tuotteet tuotteet = db.Tuotteet.Find(id);
                if (tuotteet == null)
                {
                    return HttpNotFound();
                }
                return View(tuotteet);

            }
            
        }

        // POST: Tuotteets/Delete/5
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
                Tuotteet tuotteet = db.Tuotteet.Find(id);
                db.Tuotteet.Remove(tuotteet);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
           
        }
        //----------------------------------------------------------DISPOSE DB-------------------------------------------------------------------

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
