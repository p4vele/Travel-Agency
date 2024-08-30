using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tiksoret.Models;

namespace Tiksoret.Controllers
{
    public class FlightBookController : Controller
    {
        // GET: FlightBook
        private OurDBContext db = new OurDBContext();
        public ActionResult Index()
        {
            ViewBag.dcity = db.TicketReserve_tbl.Select(l => l.ResFrom).Distinct().ToList();
            ViewBag.acity = db.TicketReserve_tbl.Select(l => l.ResTo).Distinct().ToList();

            return View();
        }


        
        [HttpPost]
        public ActionResult search(string cityto, string cityfrom, string date1,string sort)
        {
            //no infp
            if(cityto== "To Destenation" && cityfrom== "From Destenation" &&sort=="Sort By")
            {
                var c = db.TicketReserve_tbl.ToList();
                ViewBag.ss = c;
            }
            //sort all by price up
            else if (cityto == "To Destenation" && cityfrom == "From Destenation" && sort == "Price Up")
            {
                var c = db.TicketReserve_tbl.ToList().OrderBy(s=>s.ResTicketPrice);
                ViewBag.ss = c;

            }
            //sort all by price down
            else if (cityto == "To Destenation" && cityfrom == "From Destenation" && sort == "Price Down")
            {
                var c = db.TicketReserve_tbl.ToList().OrderByDescending(s => s.ResTicketPrice);
                ViewBag.ss = c;

            }
            //sort all by  country
            else if (cityto == "To Destenation" && cityfrom == "From Destenation" && sort == "Country")
            {
                var c = db.TicketReserve_tbl.ToList().OrderBy(s => s.ResFrom);
                ViewBag.ss = c;

            }
            
            //show flights on that date sort by price up
            else if (date1.Length > 0 && sort == "Price Up")
            {
                var c = db.TicketReserve_tbl.Where(l => l.ResTo.Equals(cityto) && l.ResFrom.Equals(cityfrom) && l.ResDepDate.Equals(date1)).OrderBy(s => s.ResTicketPrice);
                ViewBag.ss = c;
            }
            //show flights on that date sort by price down
            else if (date1.Length > 0 && sort == "Price Down")
            {
                var c = db.TicketReserve_tbl.Where(l => l.ResTo.Equals(cityto) && l.ResFrom.Equals(cityfrom) && l.ResDepDate.Equals(date1)).OrderByDescending(s => s.ResTicketPrice); ;
                ViewBag.ss = c;
            }
            //show flights on that date sort by country
            else if (date1.Length > 0 && sort == "Country")
            {
                var c = db.TicketReserve_tbl.Where(l => l.ResTo.Equals(cityto) && l.ResFrom.Equals(cityfrom) && l.ResDepDate.Equals(date1)).OrderBy(s => s.ResFrom); 
                ViewBag.ss = c;
            }
            //from dont sort
            else if(cityfrom != "From Destenation" && sort == "Sort By")
            {
                var c = db.TicketReserve_tbl.Where(l =>  l.ResFrom.Equals(cityfrom));
                ViewBag.ss = c;
            }
            //to dont sort
            else if (cityto != "To Destenation" && sort == "Sort By")
            {
                var c = db.TicketReserve_tbl.Where(l => l.ResFrom.Equals(cityto));
                ViewBag.ss = c;
            }
            //from with date no sort
            else if (cityfrom != "From Destenation" && sort == "Sort By" && date1.Length > 0)
            {
                var c = db.TicketReserve_tbl.Where(l => l.ResFrom.Equals(cityfrom) && l.ResDepDate.Equals(date1));
                ViewBag.ss = c;
            }
            //to with date no sort
            else if (cityto != "To Destenation" && sort == "Sort By" && date1.Length>0)
            {
                var c = db.TicketReserve_tbl.Where(l => l.ResFrom.Equals(cityto) && l.ResDepDate.Equals(date1));
                ViewBag.ss = c;
            }
            //show flights on that date
            else if (date1.Length > 0 && cityto == "To Destenation" && cityfrom == "From Destenation" && sort == "Sort By")
            {
                var c = db.TicketReserve_tbl.Where(l => l.ResDepDate.Equals(date1));
                ViewBag.ss = c;

            }
            else
            {
                var c = db.TicketReserve_tbl.Where(l => l.ResTo.Equals(cityto) && l.ResFrom.Equals(cityfrom));
                ViewBag.ss = c;
            }
            

            return View();
        }



      



    }
}