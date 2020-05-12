using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_03_tilausdb.Models;
using System.Net;
using System.Data.Entity;

namespace WebApp_03_tilausdb.Controllers
{
    
    public class PostitoimipaikatController : Controller
    {
        // GET: Postitoimipaikat
        TilausDBEntities db = new TilausDBEntities();

        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                List<Postitoimipaikat> model = db.Postitoimipaikat.ToList();
                return View(model);

            }

           
            
        }
        //---------------------------------------------------------------------------CREATE-------------------------------------------

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Postinumero,Postitoimipaikka")] Postitoimipaikat uusiPaikka)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Postitoimipaikat.Add(uusiPaikka);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(uusiPaikka);

            }
          
        }
        //---------------------------------------------------------------------------EDIT-------------------------------------------
        public ActionResult Edit(string id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Postitoimipaikat muokattavaPaikka = db.Postitoimipaikat.Find(id);
                if (id == null) return HttpNotFound();
                return View(muokattavaPaikka);

            }
            
           
        }
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Postinumero,Postitoimipaikka")] Postitoimipaikat muokattavaPaikka)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(muokattavaPaikka).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(muokattavaPaikka);

            }
            
        }
        //---------------------------------------------------------------------------DELETE-------------------------------------------

        public ActionResult Delete(string id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Postitoimipaikat poistettavaPaikka = db.Postitoimipaikat.Find(id);
                if (id == null) return HttpNotFound();
                return View(poistettavaPaikka);

            }
           

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                Postitoimipaikat poistettavaPaikka = db.Postitoimipaikat.Find(id);
                db.Postitoimipaikat.Remove(poistettavaPaikka);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
          
            
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