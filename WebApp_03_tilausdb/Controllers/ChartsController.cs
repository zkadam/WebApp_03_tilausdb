using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_03_tilausdb.Models;
using WebApp_03_tilausdb.ViewModel;

namespace WebApp_03_tilausdb.Controllers
{
    public class ChartsController : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();
        // GET: Charts
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult TilausMaariaArkipaivissa()
        {

            string daysMaPeList;
            string tilauksetMaPeList;//huom pieni t alussa eli tää on vain string
            List<arkiMyyntiClass> TilauksetMaPeList = new List<arkiMyyntiClass>();

            var arkiSales = from arkis in db.tilaus_maaria_arkipaivissa
                            select arkis;
            foreach (tilaus_maaria_arkipaivissa tilauksetMaPe in arkiSales)
            {
                arkiMyyntiClass yksKentta = new arkiMyyntiClass();
                yksKentta.viikkopaiva = tilauksetMaPe.viikkopaiva;//nämä ovat databasein tableviewin kentät
                yksKentta.number_of_orders = tilauksetMaPe.number_of_orders;//nämä ovat databasein tableviewin kentät
                TilauksetMaPeList.Add(yksKentta);
            }
            //string joinnilla viedään kaikki list elementtit kaks string riviin x ja y varteen- laitetaan pilkut ja qmarksit väliin
            daysMaPeList = "'" + string.Join("','", TilauksetMaPeList.Select(n => n.viikkopaiva).ToList()) + "'";
            tilauksetMaPeList = string.Join(",", TilauksetMaPeList.Select(n => n.number_of_orders).ToList());

            ViewBag.daysMaPe = daysMaPeList;
            ViewBag.TilauksetMaPe = tilauksetMaPeList;
            return View();
        }



        public ActionResult top10tuote()
        {

            string topProductNamesList;
            string productsSaleSumList;//huom pieni t alussa eli tää on vain string
            List<top10class> Top10ProductsList = new List<top10class>();

            var bestProds = from arkis in db.top10_tuote_income
                            select arkis;
            foreach (top10_tuote_income topTuote in bestProds)
            {
                top10class yksKentta = new top10class();
                yksKentta.product_name = topTuote.product_name;//nämä ovat databasein tableviewin kentät
                yksKentta.income_from_product = (int)topTuote.income_from_product;//nämä ovat databasein tableviewin kentät
                Top10ProductsList.Add(yksKentta);
            }
            //string joinnilla viedään kaikki list elementtit kaks string riviin x ja y varteen- laitetaan pilkut ja qmarksit väliin
            topProductNamesList = "'" + string.Join("','", Top10ProductsList.Select(n => n.product_name).ToList()) + "'";
            productsSaleSumList = string.Join(",", Top10ProductsList.Select(n => n.income_from_product).ToList());

            ViewBag.productNames = topProductNamesList;
            ViewBag.saleSumList = productsSaleSumList;
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