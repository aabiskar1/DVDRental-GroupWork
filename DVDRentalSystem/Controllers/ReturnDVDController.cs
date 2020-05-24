using DataContext.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVDRentalSystem.Controllers
{
    [Authorize(Roles = "Manager,Assistant")]
    public class ReturnDVDController : Controller
    {
        private DVDRentalSystemContext dbCon = new DVDRentalSystemContext();
        // GET: ReturnDVD
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReturnDVD() {
            var movieList = dbCon.DVDDetails.Select(x => x.Name);
            ViewBag.MovieLists = movieList.ToList();



            return View();
        }

    }
}