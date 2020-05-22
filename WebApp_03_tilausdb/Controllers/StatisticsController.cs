using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_03_tilausdb.Models;

namespace WebApp_03_tilausdb.Controllers
{
    public class StatisticsController : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();
        // GET: Statistics
        public ActionResult Index()
        {
            var top10Products = from tt in db.top10_tuote_income
                                select tt;
            return View(top10Products.ToList());
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