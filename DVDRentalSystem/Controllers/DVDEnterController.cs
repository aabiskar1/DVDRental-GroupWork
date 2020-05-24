using DataContext.Data;
using DVDRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVDRentalSystem.Controllers
{
    [Authorize(Roles = "Manager,Assistant")]
    public class DVDEnterController : Controller
    {
        private readonly DVDRentalSystemContext db = new DVDRentalSystemContext();
        // GET: DVDEnter
        public ActionResult Index()
        {

            var DVDDetailsList = db.DVDDetails.ToList();
            var CastDetailsList = db.CastDetails.ToList();
            var allData = db.Producers.Include("DVDDetails").Include("CastDetails");


            ViewBag.DVDDetails = new SelectList(DVDDetailsList, "DVDDetailsId", "Name");
            ViewBag.CastDetails = new SelectList(CastDetailsList, "CastDetailsId", "LastName");
           
            return View();
        }

        public ActionResult Master() 
        {



            var DVDDetailsList = db.DVDDetails.ToList();
            var CastDetailsList = db.CastDetails.ToList();


            ViewBag.DVDDetails = new SelectList(DVDDetailsList, "DVDDetailsId", "Name");
            ViewBag.CastDetails = new SelectList(CastDetailsList, "CastDetailsId", "LastName");


            var allData = db.Producers.Include("DVDDetails").Include("CastDetails");

            



            return View();
        }
    }




}