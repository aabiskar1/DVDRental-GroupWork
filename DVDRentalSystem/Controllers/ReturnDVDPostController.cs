using DataContext.Data;
using DVDRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVDRentalSystem.Controllers
{
    [Authorize(Roles = "Manager,Assistant")]
    public class ReturnDVDPostController : Controller
    {
        private DVDRentalSystemContext dbCon = new DVDRentalSystemContext();
        // GET: ReturnDVDPost
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CopyIdList(string dvdName)
        {
            dvdName = HttpContext.Request["DVDDetails"];
            var copyNumber = dbCon.Loans.Include("DVDDetails")
                .Where(x => x.DVDDetails.Name == dvdName)
                .Where(x => x.ActualReturnedDate == null)
                .Select(x => x.CopyId);
            ViewBag.CopyId = copyNumber.ToList();


            TempData["ReturnDVDName"] = dvdName;

            return View();

        }

        public ActionResult ShowReturnDetails(string copyid)
        {


            string dvdName = TempData["ReturnDVDName"] as string;

            TempData["ReturnDVDName"] = dvdName;
            TempData["ReturnCopyID"] = copyid;

            int intCopyId = int.Parse(copyid);

            var loanReport = dbCon.Loans.Include("DVDDetails")
                .Where(x => x.DVDDetails.Name == dvdName)
                .Where(x => x.CopyId == intCopyId)
                .Where(x=> x.ActualReturnedDate== null)
              
                ;



           
            return View(loanReport);
        }



        public ActionResult ReturnDVDFinal()
        {
            string dvdName = TempData["ReturnDVDName"] as string;
            string copyId = TempData["ReturnCopyID"] as string;
            int parsedCopyId = int.Parse(copyId);

            var loanedRow = dbCon.Loans.Where(x => x.DVDDetails.Name == dvdName)
                .Where(x => x.ActualReturnedDate == null)
                .Where(x => x.CopyId == parsedCopyId).SingleOrDefault();

            if (loanedRow != null)
            {
                loanedRow.ActualReturnedDate = DateTime.Now;

                dbCon.SaveChanges();
            }

            return RedirectToAction("index", "Loans");
        }
    }
}