using DataContext.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVDRentalSystem.Controllers
{
    public class FilterByProducerDVDReleaseCastController : Controller
    {
        // GET: FilterByProducerDVDReleaseCast
        private DVDRentalSystemContext dbCon = new DVDRentalSystemContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult filterProducerByDVDCast() {
            var dvdSort = dbCon.Producers.Include("CastDetails").Include("DVDDetails").OrderBy(x=>x.DVDDetails.ReleaseDate).ThenBy(x => x.CastDetails.LastName).ToList();
            return View(dvdSort);



        }

    }
}