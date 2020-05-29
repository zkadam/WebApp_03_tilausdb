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
    public class StatisticCharts : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();

        // GET: StatisticCharts
        public ActionResult TilausMaariaArkipaivissa()
        {

            string daysMaPeList;
            //string TilauksetMaPeList;
            List<tilaus_maaria_arkipaivissa> TilauksetMaPeList = new List<tilaus_maaria_arkipaivissa>();

            var arkiSales = from arkis in db.tilaus_maaria_arkipaivissa
                                    select arkis;
            foreach (tilaus_maaria_arkipaivissa tilauksetMaPe in arkiSales)
            {
                tilaus_maaria_arkipaivissa yksKentta = new tilaus_maaria_arkipaivissa();
                yksKentta.viikkopaiva = tilauksetMaPe.viikkopaiva;//nämä ovat databasein tableviewin kentät
                yksKentta.number_of_orders = tilauksetMaPe.number_of_orders;
                TilauksetMaPeList.Add(yksKentta);
            }
            //string joinnilla viedään kaikki list elementtit kaks string riviin x ja y varteen- laitetaan pilkut ja qmarksit väliin
            daysMaPeList = "'" + string.Join("','", TilauksetMaPe.Select(n => n.CategoryName).ToList()) + "'";
            TilauksetMaPeList = string.Join(",", TilauksetMaPe.Select(n => n.CategorySales).ToList());

            ViewBag.daysMaPe = daysMaPe;
            ViewBag.TilauksetMaPe = TilauksetMaPe;
            return View();
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
